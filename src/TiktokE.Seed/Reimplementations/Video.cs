using System;
using System.Collections.Generic;
using System.Text;

namespace TiktokE.Seed.Reimplementations
{
  public class Video : Core.TT.Video
  {
    #region Constructors
    public Video() : base()
    {
    }
    public Video(string href)
    {
      // https://www.tiktok.com/@meredithduxbury/video/6950377751517891846
      this.Href = href;
      var r = new System.Text.RegularExpressions.Regex("@([^/]*)/video/(\\d*)");
      if(!r.IsMatch(href))
        throw new Exception("Unsupported href format for TT.Video");
      var groups = r.Match(href).Groups;
      this.HandleID = groups[1].ToString();
      this.TTID = Convert.ToInt64(groups[2].ToString());
    }
    #endregion
    public static implicit operator Video(string href) => new Video(href);

  }
}
