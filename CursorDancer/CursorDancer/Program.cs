using CursorDancer.Replays;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.IO;
using CursorDancer.Dancers;
using CursorDancer.Models;
using System.Collections.Generic;
using SevenZip.Compression.LZMA;

namespace CursorDancer
{
  class Program
  {
    // fps of the replay
    public const int REPLAY_FPS = 15;

    static void Main(string[] args)
    {
      Console.WriteLine("");

      // Check if all arguments have been set
      if (args.Length != 4) // <.osu file> <dancer> <mods> <output>
      {
        Console.WriteLine("Invalid usage of arguments.");
        Console.WriteLine("Please specify ALL arguments in this order:");
        Console.WriteLine("- Input .osu file");
        Console.WriteLine("- ID of the dancer");
        Console.WriteLine("- Mods (e.g. HDDT, EZPF etc.)");
        Console.WriteLine("- Output .osr file");
        Console.WriteLine("");
        return;
      }

      string beatmapfile = args[0];

      // Check if the beatmap file exists
      if (!File.Exists(beatmapfile))
      {
        Console.WriteLine("The specified beatmap file could not be found.");
        Console.WriteLine("");
        return;
      }

      string dancerID = args[1];

      // Initialize cursor dancer manager
      CursorDancerManager.Initialize();

      // Get the cursor dancer
      ICursorDancer cursordancer = CursorDancerManager.GetCursorDancer(dancerID);

      // Check if dancer exists
      if (cursordancer == null)
      {
        Console.WriteLine($"The specified dancer ID ('{dancerID}') could not be found.");
        Console.WriteLine("");
        return;
      }

      string readableMods = args[2];

      // Try to parse the mods
      Mods mods = Mods.None;
      Dictionary<string, Mods> modsLookupTable = new Dictionary<string, Mods>() // Lookup table to convert readable mods into mods
      {
        { "NM", Mods.None },
        { "NF", Mods.NoFail },
        { "EZ", Mods.Easy },
        { "HD", Mods.Hidden },
        { "HR", Mods.HardRock },
        { "SD", Mods.SuddenDeath },
        { "DT", Mods.DoubleTime },
        { "RX", Mods.Relax },
        { "HT", Mods.HalfTime },
        { "NC", Mods.DoubleTime | Mods.Nightcore },
        { "FL", Mods.Flashlight },
        { "SO", Mods.SpunOut },
        { "AP", Mods.AutoPilot },
        { "PF", Mods.Perfect }
      };

      // Get 2 chars at a time
      for (int i = 0; i < readableMods.Length; i += 2)
      {
        // Check if two chars can be get or if the string ends
        // If the string ends there is a mod with only 1 letter => Invalid mods
        if (i + 1 == readableMods.Length)
        {
          Console.WriteLine($"Invalid Mod: '{readableMods[i]}'");
          Console.WriteLine("");
          return;
        }

        // Get the readable mod and check if it exists in the lookup table
        string readableMod = $"{readableMods[i]}{readableMods[i + 1]}";
        if (!modsLookupTable.ContainsKey(readableMod))
        {
          Console.WriteLine($"Invalid Mod: '{readableMod}'");
          Console.WriteLine("");
          return;
        }

        // Add the mod to the mods
        mods = mods | modsLookupTable[readableMod];
      }

      string replayfile = args[3];

      // Check if the output replay file already exists
      if (File.Exists(replayfile))
      {
        Console.WriteLine("The specified output replay file already exists!");
        Console.WriteLine("Press enter to overwrite the file, otherwise press CTRL + C");
        Console.ReadLine();
      }

      // Get the beatmap hash
      string beatmaphash = string.Join("", MD5.Create().ComputeHash(File.ReadAllBytes(args[0])).Select(x => x.ToString("x2")));

      // Parse the beatmap into beatmap informations
      string beatmap = File.ReadAllText(args[0]);
      BeatmapInformations infos = null;

      try
      {
        // get all lines
        string[] beatmapLines = beatmap.Replace("\r", "").Split('\n');

        // parse the difficulty stuff
        double cs = double.Parse(beatmapLines.First(x => x.StartsWith("CircleSize")).Split(':')[1]);
        double ar = double.Parse(beatmapLines.First(x => x.StartsWith("ApproachRate")).Split(':')[1]);
        double od = double.Parse(beatmapLines.First(x => x.StartsWith("OverallDifficulty")).Split(':')[1]);
        double hp = double.Parse(beatmapLines.First(x => x.StartsWith("HPDrainRate")).Split(':')[1]);

        string[] rawhitobjects = beatmapLines.SkipWhile(x => x != "[HitObjects]").Skip(1).TakeWhile(x => x != "").ToArray();

        List<HitObject> hitobjects = new List<HitObject>();
        foreach (string raw in rawhitobjects)
          hitobjects.Add(new HitObject(raw));

        infos = new BeatmapInformations(cs, ar, od, hp, hitobjects);
      }
      catch (Exception ex)
      {
        Console.WriteLine("An error occured whilst trying to parse the beatmap.");
        Console.WriteLine("Please make sure the chosen .osu file is an osu!standard beatmap and is not corrupted.");
        Console.WriteLine("");
        Console.WriteLine("If you think this is an issue with the program, please create an issue on github and add the following informations:");
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine($"Exception Message: {ex.Message}");
        Console.WriteLine("");
        Console.WriteLine("Stack Trace:");
        Console.WriteLine(ex.StackTrace);
        Console.WriteLine("");
        Console.WriteLine("https://github.com/minisbett/cursordancer/");
        Console.WriteLine("");
        return;
      }

      // Retrieve replay score data from the dancer
      ReplayScore score = cursordancer.GetReplayScore();

      // Retrieve the replay frames from the danser
      List<ReplayFrame> frames = cursordancer.GetReplayFrames(infos, REPLAY_FPS);

      // Add some required frames
      frames.Insert(0, new ReplayFrame(0, 256, -500, Keys.None)); // idk what that is
      frames.Insert(1, new ReplayFrame(-1, 256, -500, Keys.None)); // idk what that is
      frames.Insert(2, new ReplayFrame(666666, 666, 666, Keys.None));
      frames.Add(new ReplayFrame(-12345, 0, 0, Keys.None)); // Add mania random seed (Keys.None = 0)

      // convert the replay frames into the raw string, ready to be compressed
      string rawReplayFrames = string.Join(',', frames.Select(x => x.ToString()));

      rawReplayFrames = rawReplayFrames.Replace("666666|666|666|0", $"-300|-48.66667|32|0");

      byte[] compressedReplayFrames = SevenZipHelper.Compress(Encoding.UTF8.GetBytes(rawReplayFrames));

      Replay replay = new Replay()
      {
        Count300 = score.Count300,
        Count100 = score.Count100,
        Count50 = score.Count50,
        CountGeki = score.CountGeki,
        CountKatu = score.CountKatu,
        CountMiss = score.CountMiss,
        FullCombo = score.FullCombo,
        MaxCombo = score.MaxCombo,
        PlayerName = cursordancer.Name,
        Mode = 0,
        Version = 20210316,
        UsedMods = mods,
        Score = score.Score,
        ReplayDate = DateTime.UtcNow,
        BeatmapHash = beatmaphash,
        ReplayData = compressedReplayFrames
      };

      /* string hash = string.Join("", MD5.Create().ComputeHash(System.IO.File.ReadAllBytes("C:\\Users\\Niklas\\Desktop\\delis.osu")).Select(x => x.ToString("x2")));

      Replay replay = new Replay()
      {
        Count300 = 301,
        Count100 = 22,
        Count50 = 0,
        CountGeki = 46,
        CountKatu = 15,
        CountMiss = 7,
        FullCombo = false,
        MaxCombo = 343,
        PlayerName = "minisbett",
        Mode = 0,
        Version = 20210316,
        UsedMods = Mods.None,
        Score = 1960948,
        ReplayDate = new DateTime(637521326729822934, DateTimeKind.Utc),
        PerformanceGraphData = "",
        BeatmapHash = hash,
        ReplayData = System.IO.File.ReadAllBytes("C:\\Users\\Niklas\\Desktop\\raw.txt")
      }; */

      byte[] bytes = replay.GetBytes();
      File.WriteAllBytes("C:\\Users\\Niklas\\Desktop\\test.osr", bytes);

      Console.WriteLine("Export successful.");
      Console.WriteLine("");
    }
  }
}
