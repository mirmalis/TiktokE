using System;
using System.Collections.Generic;
using System.Text;

namespace TiktokE.Core.TT
{
  public class ChannelHandle : Timed
  {
    public Channel Channel { get; set; } public Guid ChannelID { get; set; }
    public Handle Handle { get; set; } public string HandleID { get; set; }
  }
}
