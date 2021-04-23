using System;
using System.Collections.Generic;
namespace TiktokE.Core.TT
{
  public abstract class Timed : TikTokEntity, ITimed
  {
    #region Constructors
    public Timed() { }
    public Timed(ulong ttid) : base(ttid)
    {

    }
    #endregion
    public DateTime? Until { get; set; }
    public DateTime Recorded { get; set; } = DateTime.Now;
    public DateTime LastSeen { get; set; } = DateTime.Now;
    
  }
}