using System;
using System.Collections.Generic;
using System.Text;

namespace TiktokE.Seed.Reimplementations
{
  public class Channel : Core.TT.Channel
  {
    #region Constructors
    public Channel() { }
    public Channel(string name, Core.TT.Handle handle) : this()
    {
      if(handle == null)
        throw new Exception("Channel must have a handle");
      this.Name = name;

      this.Handles = new List<Core.TT.ChannelHandle>() {
        new Core.TT.ChannelHandle(){ Handle = handle }
      };
    }
    public Channel(string name, string handleName = null) : this(name, new Handle(handleName ?? name))
    {
    }
    #endregion
  }
}
