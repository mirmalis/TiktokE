using System;
using System.Collections.Generic;

namespace TiktokE.Core.TT
{
  public class Audio  : IDed, ITTIDed<long>
  {
    public string Name { get; set; }
    public long TTID { get; set; }

    public ICollection<Video> Videos { get; set; }
  }
}
