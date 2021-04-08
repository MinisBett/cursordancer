using System;
using System.Collections.Generic;
using System.Text;

namespace CursorDancer.Models
{
  public class Spinner : HitObject
  {

    /// <summary>
    /// The offset from the beatmap starts at which the spinner ends (substract the spinner offset from this to get the spinner length)
    /// </summary>
    public int EndTime { get; }

    public Spinner(string raw) : base(raw) // For lookup: https://osu.ppy.sh/wiki/sk/osu!_File_Formats/Osu_(file_format)#spinners
    {
      EndTime = int.Parse(raw.Split(',')[5]);
    }
  }
}
