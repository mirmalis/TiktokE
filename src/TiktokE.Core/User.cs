using System;
using System.Collections.Generic;

namespace TiktokE.Core
{
  public class User : IDed
  {
    public string Name { get; set; }
    public ICollection<UploaderPreference> ChannelPreferencess { get; set; }
    public ICollection<TagPreference> TagPreferences { get; set; }
    public ICollection<UserVideoInteraction> VideoInteractions { get; set; }
  }
}
