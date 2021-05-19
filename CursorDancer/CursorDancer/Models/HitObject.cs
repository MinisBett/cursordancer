using CursorDancer.Enums;
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
    public string ObjectParams { get; }

    /// <summary>
    /// Type of the hitobject (circle, slider, spinner)
    /// </summary>
    public HitObjectType Type { get; }

    // Save the raw hitobject to pass it to a Slider or Spinner object in the AsSlider() and AsSpinner() method
    private string m_raw = "";

    public HitObject(string raw) // For lookup: https://osu.ppy.sh/wiki/sk/osu!_File_Formats/Osu_(file_format)#hit-circles
    {
      m_raw = raw;
      string[] split = raw.Split(',');
      X = int.Parse(split[0]);
      Y = int.Parse(split[1]);
      Offset = int.Parse(split[2]);
      ObjectParams = split[5];

      int typeInt = int.Parse(raw.Split(',')[3]);

      foreach(HitObjectType type in Enum.GetValues(typeof(HitObjectType)))
      {
        if ((typeInt & (1 << (int)type)) > 0)
          Type = type;
      }
    }

    // There is no AsCircle() Method because circles do not have any additional object parameters besides the time and coordinates

    /// <summary>
    /// Converts the hit object into a slider object which provides informations about the slider (parsed object parameters)
    /// </summary>
    /// <returns>The slider object</returns>
    public Slider AsSlider()
    {
      if (Type != HitObjectType.Slider)
        throw new InvalidCastException($"The target hit object is of type '{Type}', therefore it is not a slider.");
      return new Slider(m_raw);
    }

    /// <summary>
    /// Converts the hit object into a spinner object which provides informations about the slider (parsed object parameters)
    /// </summary>
    /// <returns>The spinner object</returns>
    public Spinner AsSpinner()
    {
      if (Type != HitObjectType.Spinner)
        throw new InvalidCastException($"The target hit object is of type '{Type}', therefore it is not a spinner.");
      return new Spinner(m_raw);
    }
  }
}
