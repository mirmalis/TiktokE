//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.EntityFrameworkCore;

//namespace TiktokE.Api1.Helpers.XX
//{
//  public class Deep : Types.XX.Deep
//  {
//    #region Constructors
//    public Deep() { }
//    public Deep(Guid? id, Core.XX core) : base()
//    {
//      this.ID = Calc.ID(id, core);
//      if (core == null)
//        return;
//    }
//    #endregion
//    public static IQueryable<Core.XX> Includes(IQueryable<Core.XX> Q)
//      => Q
//    ;
//  }
//  public class Shallow : Types.XX.Shallow
//  {
//    #region Constructors
//    public Shallow() { }
//    public Shallow(Guid? id, Core.XX core) : base()
//    {
//      this.ID = Calc.ID(id, core);
//      if (core == null)
//        return;
//    }
//    #endregion
//    public static IQueryable<Core.XX> Includes(IQueryable<Core.XX> Q)
//      => Q
//    ;
//  }
//}
