using CursorDancer.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CursorDancer.Models
{
  public class Slider : HitObject
  {

    /// <summary>
    /// Type of the slider curve (bezier, centripetal, linear, perfect circle)
    /// </summary>
    public CurveType CurveType { get; } = CurveType.None;

    /// <summary>
    /// The coordinates of the curve points of the slider
    /// </summary>
    public (int x, int y)[] CurvePoints { get; }

    /// <summary>
    /// Amount of times the player has to follow the slider's curve back-and-forth
    /// </summary>
    public int Slides { get; }

    /// <summary>
    /// The visual length of the slider in osu! pixels (512x386 playfield area)
    /// </summary>
    public decimal Length { get; }

    public Slider(string raw) : base(raw) // For lookup: https://osu.ppy.sh/wiki/sk/osu!_File_Formats/Osu_(file_format)#sliders
    {
      string[] split = raw.Split(',');

      string curvetype = split[5].Split('|')[0]; // curvetype|x:y|x:y|...
      if (curvetype == "B")
        CurveType = CurveType.Bezier;
      else if (curvetype == "C")
        CurveType = CurveType.CentripetalCatmullRom;
      else if (curvetype == "L")
        CurveType = CurveType.Linear;
      else if (curvetype == "P")
        CurveType = CurveType.PerfectCircle;

      string curvePointsStr = split[5].Split("|".ToCharArray(), 2)[1]; // curvetype|x:y|x:y|...
      List<(int x, int y)> curvepoints = new List<(int x, int y)>();
      foreach (string curvepoint in curvePointsStr.Split('|'))
        curvepoints.Add((int.Parse(curvepoint.Split(':')[0]), int.Parse(curvepoint.Split(':')[1])));
      CurvePoints = curvepoints.ToArray();

      Slides = int.Parse(split[6]);
      Length = decimal.Parse(split[7], new CultureInfo("en-US"));
    }
  }
}
