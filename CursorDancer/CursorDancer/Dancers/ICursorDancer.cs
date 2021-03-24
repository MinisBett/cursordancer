using CursorDancer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursorDancer.Dancers
{
  public interface ICursorDancer
  {
    public string ID { get; }

    public string Name { get; }

    public ReplayScore GetReplayScore();

    public List<ReplayFrame> GetReplayFrames(BeatmapInformations beatmapInformations, int fps);
  }
}
