using System;
using System.Collections.Generic;
namespace TiktokE.Core.TT
{
  public class Handle : IIDed<string>
  {
    #region Constructors
    public Handle() : base() { }
    public Handle(string handle)
    {
      this.ID = handle;
    }
    #endregion
    public string ID { get; set; }
    public ICollection<Channel_Handle> ChannelAssignments { get; set; }
    public ICollection<Video> Videos { get; set; }
  }
}
