using System;
using System.Collections.Generic;
namespace TiktokE.Core.TT
{
  public class Handle : IIDed<string>
  {
    public string ID { get; set; }
    public string Name { get; set; }

    public ICollection<ChannelHandle> Channels { get; set; }
    public ICollection<Video> Videos { get; set; }
  }
}
