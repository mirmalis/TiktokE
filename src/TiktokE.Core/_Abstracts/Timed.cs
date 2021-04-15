using System;
using System.Collections.Generic;
namespace TiktokE.Core
{
  public class Timed : IDed
  {
    public DateTime? Since { get; set; }
    public DateTime? Until { get; set; }
    public DateTime Recorded { get; set; } = DateTime.Now;
    public DateTime LastSeen { get; set; } = DateTime.Now;
    public bool IsActive(DateTime? dt)
    {
      return (Since == null || Since <= dt) && (Until == null || Until > dt);
    }
  }
}