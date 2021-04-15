using System;
using System.Collections.Generic;
namespace TiktokE.Core.TT
{
  public class Channel : IDed, ITTIDed<long?>
  {
    public string Name { get; set; }
    public long? TTID { get; set; }
    public bool Seen { get; set; }
    public ICollection<ChannelHandle> Handles { get; set; }

    public ICollection<Comment> Comments { get; set; }

  }
}

