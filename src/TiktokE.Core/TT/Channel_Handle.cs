using System;
using System.Collections.Generic;

namespace TiktokE.Core.TT
{
  public class Channel_Handle : IDed, ITimed
  {
    public Channel Channel { get; set; } public Guid ChannelID { get; set; }
    public Handle Handle { get; set; } public string HandleID { get; set; }

    public DateTime Since { get; set; } = DateTime.Now;
    public DateTime? Until { get; set; }
    public DateTime Recorded { get; set; } = DateTime.Now;
    public DateTime LastSeen { get; set; } = DateTime.Now;
    public bool IsActive(DateTime dt)
    {
      return (Since <= dt) && (Until == null || Until > dt);
    }
  }
}