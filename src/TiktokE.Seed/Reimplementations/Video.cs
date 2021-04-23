using System;
using System.Collections.Generic;
namespace TiktokE.Seed.Reimplementations
{
  public class Video : Core.TT.Video
  {
    #region Constructors
    public Video() : base(){ }
    public Video(string href) : base(href){ }
    #endregion
    public static implicit operator Video(string href) => new Video(href);

  }
}
