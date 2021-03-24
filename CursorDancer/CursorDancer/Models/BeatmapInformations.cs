using System;
using System.Collections.Generic;
using System.Text;

namespace CursorDancer.Models
{
  public class BeatmapInformations
  {
    /// <summary>
    /// Circle size of the beatmap
    /// </summary>
    public double CS { get; }

    /// <summary>
    /// Radius of a circle on the beatmap (in osu! pixels)
    /// </summary>
    public int CSRadius => (int)Math.Round(54.4 - 4.48 * CS);

    /// <summary>
    /// Approach rate of the beatmap
    /// </summary>
    public double AR { get; }

    /// <summary>
    /// Overall difficulty of the beatmap
    /// </summary>
    public OverallDifficulty OD { get; }

    /// <summary>
    /// HP drain of the beatmap
    /// </summary>
    public double HP { get; }

    /// <summary>
    /// The beatmaps hit objects
    /// </summary>
    public List<HitObject> HitObjects { get; }

    public BeatmapInformations(double cs, double ar, double od, double hp, List<HitObject> hitObjects)
    {
      CS = cs;
      AR = ar;
      OD = new OverallDifficulty(od);
      HP = hp;
      HitObjects = hitObjects;
    }
  }
}
