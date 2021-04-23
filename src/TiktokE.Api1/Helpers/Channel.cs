using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TiktokE.Api1.Helpers.Channel
{
  namespace Post
  {
    public class TTID_Handle_Name : Types.Channel.Post.TTID_Handle_Name
    {
      internal Core.TT.Channel Item
      {
        get
        {
          if (!ulong.TryParse(this.TTID, out var ttid))
          {
            throw new Exception("Invalid TTID.");
          }
          return new Core.TT.Channel()
          {
            TTID = ttid,
            ActiveHandleID = this.Handle,
            Name = this.Name
          };
        }
      }
    }
  }
  public class Deep : Types.Channel.ChannelDeep
  {
    #region Constructors
    public Deep() { }
    public Deep(Guid? id, Core.TT.Channel core) : base()
    {
      this.ID = Calc.ID(id, core);
      if (core == null)
        return;
      this.TTID = core.TTID.ToString();
      this.Name = core.Name;
      this.Handle = core.ActiveHandleID;
      this.Created = core.Since;
    }
    #endregion
    public static IQueryable<Core.TT.Channel> Includes(IQueryable<Core.TT.Channel> Q)
      => Q
        .Include(item => item.Handles).ThenInclude(item => item.Handle).ThenInclude(item => item.Videos)
    ;
  }
  public class Shallow : Types.Channel.ChannelShallow
  {
    #region Constructors
    public Shallow() { }
    public Shallow(Guid? id, Core.TT.Channel core) : base()
    {
      this.ID = Calc.ID(id, core);
      if (core == null)
        return;
      this.TTID = core.TTID.ToString();
      this.Handle = core.ActiveHandleID;
    }
    #endregion
    public static IQueryable<Core.TT.Channel> Includes(IQueryable<Core.TT.Channel> Q)
      => Q
    ;
  }
}