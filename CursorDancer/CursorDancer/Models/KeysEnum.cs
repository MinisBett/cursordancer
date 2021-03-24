using System;
using System.Collections.Generic;
using System.Text;

namespace CursorDancer.Models
{
  public enum Keys
  {
    None = 0,
    M1 = 1,
    M2 = 2,
    K1 = 5, // because you can only press either M1 or K1 => 1 + 4
    K2 = 10, // because you can only press either M2 or K2 => 2 + 8
    Smoke = 16
  }
}
