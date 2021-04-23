using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TiktokE.Api1.Helpers.Video
{
  namespace Get
  {
    public class Deep : Types.Video.Get.VideoDeep
    {
      #region Constructors
      public Deep() { }
      public Deep(Guid? id, Core.TT.Video core) : base()
      {
        this.ID = Calc.ID(id, core);
        if (core == null)
          return;
        this.Handle = core.HandleID;
        this.TTID = core.TTID.ToString();
      }
      #endregion
      public static IQueryable<Core.TT.Video> Includes(IQueryable<Core.TT.Video> Q)
        => Q
      ;
    }
    public class Shallow : Types.Video.Get.VideoShallow
    {
      #region Constructors
      public Shallow() { }
      public Shallow(Guid? id, Core.TT.Video core) : base()
      {
        this.ID = Calc.ID(id, core);
        if (core == null)
          return;
        this.Handle = core.HandleID;
        this.TTID = core.TTID.ToString();
      }
      #endregion
      public static IQueryable<Core.TT.Video> Includes(IQueryable<Core.TT.Video> Q)
        => Q
      ;
    }
  }

  internal static class Post
  {
    internal static Core.TT.Video Core(Types.Video.Post.TTID_Channel_Description data)
    {
      if (!ulong.TryParse(data.TTID, out ulong ttid))
      {
        throw new Exception($"TTID {data.TTID} does not parse.");
      }
      return new Core.TT.Video()
      {
        TTID = ttid,
        Description = data.Description
      };
    }
  }
}
