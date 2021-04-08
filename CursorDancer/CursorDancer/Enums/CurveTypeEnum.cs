using System;
using System.Collections.Generic;
using System.Text;

namespace CursorDancer.Enums
{
  public enum CurveType // https://osu.ppy.sh/wiki/sk/osu!_File_Formats/Osu_(file_format)#sliders
  {                     // the curve type is a letter
    None,
    Bezier, // B
    CentripetalCatmullRom, // C
    Linear, // L
    PerfectCircle // P
  }
}
