using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CursorDancer.Replays
{
  public class Replay
  {
    /// <summary>
    /// Mode (std/taiko/catch/mania) the replay is played in
    /// </summary>
    public byte Mode { get; set; }

    /// <summary>
    /// Version of the osu client that generated this replay
    /// </summary>
    public int Version { get; set; }

    /// <summary>
    /// Hash of the beatmap the replay is from
    /// </summary>
    public string BeatmapHash { get; set; }

    /// <summary>
    /// Name of the player who did the replay
    /// </summary>
    public string PlayerName { get; set; }

    /// <summary>
    /// Amount of 300s hit
    /// </summary>
    public ushort Count300 { get; set; }

    /// <summary>
    /// Amount of 100s hit
    /// </summary>
    public ushort Count100 { get; set; }

    /// <summary>
    /// Amount of 50s hit
    /// </summary>
    public ushort Count50 { get; set; }

    /// <summary>
    /// Amount of gekis hit
    /// </summary>
    public ushort CountGeki { get; set; }

    /// <summary>
    /// Amount of katus hit
    /// </summary>
    public ushort CountKatu { get; set; }

    /// <summary>
    /// Amount of gmisses
    /// </summary>
    public ushort CountMiss { get; set; }

    /// <summary>
    /// The score reached in the replaz
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// The maximum combo the player got
    /// </summary>
    public ushort MaxCombo { get; set; }

    /// <summary>
    /// Bool if the player did a full combo or not
    /// </summary>
    public bool FullCombo { get; set; }

    /// <summary>
    /// bitwise integer that determines the mods used in the replay
    /// </summary>
    public Mods UsedMods { get; set; }

    /// <summary>
    /// Date the replay was set on
    /// </summary>
    public DateTime ReplayDate { get; set; }

    /// <summary>
    /// Raw cursor and button data of the replay
    /// </summary>
    public byte[] ReplayData { get; set; }

    /// <summary>
    /// Returns the bytes of the full replay file
    /// </summary>
    /// <returns>bytes of the full replay file</returns>
    public byte[] GetBytes()
    {
      MemoryStream ms = new MemoryStream();
      using (ReplayWriter writer = new ReplayWriter(ms))
      {
        writer.Write(Mode);
        writer.Write(Version);
        writer.Write(BeatmapHash);
        writer.Write(PlayerName);
        writer.Write(""); // replay hash
        writer.Write(Count300);
        writer.Write(Count100);
        writer.Write(Count50);
        writer.Write(CountGeki);
        writer.Write(CountKatu);
        writer.Write(CountMiss);
        writer.Write(Score);
        writer.Write(MaxCombo);
        writer.Write(FullCombo);
        writer.Write((int)UsedMods);
        writer.Write(""); // performance graph data
        writer.Write(ReplayDate);
        writer.Write(ReplayData);
        writer.Write(0); // Online Play ID
        writer.Write(0); // target practice
      }

      return ms.ToArray();
    }
  }
}
