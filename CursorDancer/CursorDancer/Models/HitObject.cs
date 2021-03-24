using System;
using System.Collections.Generic;
using System.Text;

namespace CursorDancer.Models
{
  public class HitObject
  {
    /// <summary>
    /// X-Coordinate of the hitobject
    /// </summary>
    public int X { get; }

    /// <summary>
    /// Y-Coordinate of the hitobject
    /// </summary>
    public int Y { get; }

    /// <summary>
    /// Offset from the beatmapstart of the hitobject
    /// </summary>
    public int Offset { get; }

    /// <summary>
    /// Object params (e.g. spinner length, slider form etc.)
    /// </summary>
    public string ObjectParamas { get; }

    public HitObject(string raw)
    {
      string[] split = raw.Split(',');
      X = int.Parse(split[0]);
      Y = int.Parse(split[1]);
      Offset = int.Parse(split[2]);
      ObjectParamas = split[4];
    }
  }
}
