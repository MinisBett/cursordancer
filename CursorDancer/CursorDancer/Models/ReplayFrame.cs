using System;
using System.Collections.Generic;
using System.Text;

namespace CursorDancer.Models
{
  public class ReplayFrame
  {
    /// <summary>
    /// Time between this frame and the last one
    /// </summary>
    public int Offset { get; }

    /// <summary>
    /// X-coordinate of the cursor
    /// </summary>
    public int X { get; }

    /// <summary>
    /// Y-Coordinate of the cursor
    /// </summary>
    public int Y { get; }

    /// <summary>
    /// The keys pressed
    /// </summary>
    public Keys Keys { get; }

    public ReplayFrame(int offset, int x, int y, Keys keys)
    {
      Offset = offset;
      X = x;
      Y = y;
      Keys = keys;
    }

    /// <summary>
    /// Returns the replay frame in the readable standardized format
    /// </summary>
    /// <returns>The string</returns>
    public override string ToString()
    {
      return $"{Offset}|{X}|{Y}|{(int)Keys}";
    }
  }
}
