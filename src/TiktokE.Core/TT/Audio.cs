using System;
using System.Collections.Generic;

namespace TiktokE.Core.TT
{
  public class Audio  : TikTokEntity
  {
    #region Constructors
    public Audio() { }
    public Audio(ulong ttid, string name) : base(ttid)
    {
      this.Name = name;
    }
    #endregion
    public string Name { get; set; }
    public ICollection<Video> Videos { get; set; }
    public override string HrefEnd => $"music/{Name.Replace(' ', '-')}-{TTID}?";
  }
}
