using System;
using System.Collections.Generic;
namespace TiktokE.Core.TT
{
  public class Channel : Timed
  {
    #region Constructors
    public Channel() { }
    public Channel(string name, ulong ttid, Core.TT.Handle handle) : this()
    {
      this.TTID = ttid;
      this.ActiveHandle = handle ?? throw new Exception("Channel must have a handle");
      this.ActiveHandleID = handle.ID;
      this.Name = name;
      this.Handles = new List<Core.TT.Channel_Handle>() {
        new Core.TT.Channel_Handle(){
          Handle = handle
        }
      };
      this.Seen = true;
    }
    public Channel(string name, ulong ttid, string handleName = null) : this(name, ttid, new Handle(handleName ?? name))
    {
    }
    #endregion
    //https://www.tiktok.com/@bellapoarch?
    public string Name { get; set; }
    public bool? Verified { get; set; }
    public Handle ActiveHandle { get; set; } public string ActiveHandleID { get; set; }
    public ICollection<Channel_Handle> Handles { get; set; }
    //public ICollection<Comment> AuthoredComments { get; set; }
    public bool NeedsChecking { get; set; } = false;
    public bool Seen { get; set; }
    public override string HrefEnd => $"@{ActiveHandleID}?";
  }
}

