using System;
using System.Collections.Generic;
using System.Text;

namespace CursorDancer.Models
{
  public class OverallDifficulty
  {
    /// <summary>
    /// The overall difficulty
    /// </summary>
    public double Value { get; }

    /// <summary>
    /// Hitwindow for a 50 hit in ms
    /// </summary>
    public int HitWindow50 { get; }

    /// <summary>
    /// Hitwindow for a 100 hit in ms
    /// </summary>
    public int HitWindow100 { get; }

    /// <summary>
    /// Hitwindow for a 300 hit in ms
    /// </summary>
    public int HitWindow300 { get; }

    public OverallDifficulty(double value)
    {
      Value = value;
    }
  }
}
