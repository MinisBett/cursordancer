using System;
using System.Collections.Generic;
using System.Text;

namespace CursorDancer.Replays
{
  [Flags]
  public enum Mods
  {
    None = 0,
    NoFail = 1,
    Easy = 2,
    Hidden = 8,
    HardRock = 16, // 0x00000010
    SuddenDeath = 32, // 0x00000020
    DoubleTime = 64, // 0x00000040
    Relax = 128, // 0x00000080
    HalfTime = 256, // 0x00000100
    Nightcore = 512, // 0x00000200
    Flashlight = 1024, // 0x00000400
    Autoplay = 2048, // 0x00000800
    SpunOut = 4096, // 0x00001000
    AutoPilot = 8192, // 0x00002000
    Perfect = 16384, // 0x00004000
  }
}
