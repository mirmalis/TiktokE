using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TiktokE.Api1.Helpers.User
{
  public class Deep : Types.User.Deep
  {
    #region Constructors
    public Deep() { }
    public Deep(Guid? id, Core.User core) : base()
    {
      this.ID = Calc.ID(id, core);
      if(core == null)
        return;
      this.Name = core.Name;
      this.ChannelPreferencesInfo = new { core.ChannelPreferencess.Count };
      //this.TagPreferencesInfo = new { core.TagPreferences.Count };
      this.VideoInteractionsInfo = new { core.VideoInteractions.Count };
    }
    #endregion
    public static IQueryable<Core.User> Includes(IQueryable<Core.User> Q)
      => Q
        //.Include(item => item.TagPreferences)
        .Include(item => item.ChannelPreferencess)
        .Include(item => item.VideoInteractions)
    ;
  }
  public class Shallow : Types.User.Shallow
  {
    #region Constructors
    public Shallow() { }
    public Shallow(Guid? id, Core.User core) : base()
    {
      this.ID = Calc.ID(id, core);
      if(core == null)
        return;
      this.Name = core.Name;
    }
    #endregion
    public static IQueryable<Core.User> Includes(IQueryable<Core.User> Q)
      => Q
    ;
  }
}
