using System;
using System.Collections.Generic;
using System.Text;

namespace TiktokE.Core
{
  public class UploaderPreference : IDed
  {
    public User User { get; set; }     public Guid UserID { get; set; }
    public TT.Channel Channel { get; set; } public Guid ChannelID { get; set; }
    public PreferenceType Type { get; set; }
  }
}
