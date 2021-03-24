using CursorDancer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CursorDancer.Dancers
{
  public class DefaultDancer : ICursorDancer
  {
    public string ID => "defaultdancer";

    public string Name => "Default Dancer";

    public List<ReplayFrame> GetReplayFrames(BeatmapInformations beatmapInformations, int fps)
    {
      List<ReplayFrame> frames = new List<ReplayFrame>();

      int length = beatmapInformations.HitObjects.Last().Offset;

      int frame = 0;

      for(int i = 0; i < length; i++)
      {
        HitObject nextHitObject = beatmapInformations.HitObjects.First(x => x.Offset >= i); // get the next hit object
        frame++;
        if(frame == 60) // do a replay frame every (fps) frames
        {
          frame = 0;
          //                         Frame time                    X coordinate     Y coordinate     Keys
          frames.Add(new ReplayFrame((int)Math.Round(1000d / fps), nextHitObject.X, nextHitObject.Y, Keys.None));
        }
      }

      return frames;
    }

    public ReplayScore GetReplayScore()
    {
      return new ReplayScore(300, 100, 50, 101, 301, 1, 1234567890, 727, true);
    }
  }
}
