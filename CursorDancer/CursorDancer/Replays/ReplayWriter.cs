using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CursorDancer.Replays
{
  internal class ReplayWriter : BinaryWriter
  {
    internal ReplayWriter(Stream stream) : base(stream, Encoding.UTF8)
    {

    }

    public override void Write(byte[] array)
    {
      Write(array.Length);
      foreach (byte b in array)
        Write(b);
    }

    public void Write(DateTime datetime)
    {
      Write(datetime.Ticks);
    }

    public override void Write(string str)
    {
      Write((byte)11);
      base.Write(str);
    }
  }
}
