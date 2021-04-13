using System;
using System.Collections.Generic;
using System.Text;

namespace TiktokE.Seed.Reimplementations
{
  public class Handle : Core.TT.Handle
  {
    #region Constructors
    public Handle() : base()
    {

    }
    public Handle(string name)
    {
      this.ID = name;
      this.Name = name;
    }
  #endregion

}
}
