using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiktokE.Core
{
  public interface ITimed
  {
    public DateTime Since { get; }
    public DateTime? Until { get; }
    public DateTime Recorded { get; }
    public DateTime LastSeen { get; }
    public bool IsActive(DateTime? dt)
    {
      return (Since <= dt) && (Until == null || Until > dt);
    }
  }
}
