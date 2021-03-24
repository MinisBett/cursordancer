using System;
using System.Collections.Generic;
using System.Text;

namespace CursorDancer.Models
{
  public class ReplayScore
  {

    /// <summary>
    /// Amount of 300s hit
    /// </summary>
    public ushort Count300 { get; set; }

    /// <summary>
    /// Amount of 100s hit
    /// </summary>
    public ushort Count100 { get; set; }

    /// <summary>
    /// Amount of 50s hit
    /// </summary>
    public ushort Count50 { get; set; }

    /// <summary>
    /// Amount of gekis hit
    /// </summary>
    public ushort CountGeki { get; set; }

    /// <summary>
    /// Amount of katus hit
    /// </summary>
    public ushort CountKatu { get; set; }

    /// <summary>
    /// Amount of gmisses
    /// </summary>
    public ushort CountMiss { get; set; }

    /// <summary>
    /// The score reached in the replaz
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// The maximum combo the player got
    /// </summary>
    public ushort MaxCombo { get; set; }

    /// <summary>
    /// Bool if the player did a full combo or not
    /// </summary>
    public bool FullCombo { get; set; }

    public ReplayScore(ushort count300, ushort count100, ushort count50, ushort countGeki, ushort countKatu, ushort countMiss, int score, ushort maxCombo, bool fullCombo)
    {
      Count300 = count300;
      Count100 = count100;
      Count50 = count50;
      CountGeki = countGeki;
      CountKatu = countKatu;
      CountMiss = countMiss;
      Score = score;
      MaxCombo = maxCombo;
      FullCombo = fullCombo;
    }
  }
}
