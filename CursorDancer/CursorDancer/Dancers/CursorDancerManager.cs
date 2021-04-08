using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CursorDancer.Dancers
{
  public static class CursorDancerManager
  {
    private static List<ICursorDancer> m_cursordancers = new List<ICursorDancer>();

    public static void Initialize()
    {
      // TODO: use reflection to get classes from CursorDancer.Dancers namespace
      m_cursordancers.Add(new DefaultDancer());
    }

    public static ICursorDancer GetCursorDancer(string id)
    {
      return m_cursordancers.FirstOrDefault(x => x.ID == id);
    }
  }
}
