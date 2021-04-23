using System;
using System.Collections.Generic;
namespace TiktokE.Core.TT
{
  public class Video : Timed
  {
    #region Constructors
    public Video() : base()
    {
    }
    public Video(string href)
    {
      // https://www.tiktok.com/@meredithduxbury/video/6950377751517891846
      var r = new System.Text.RegularExpressions.Regex("@([^/]*)/video/(\\d*)");
      if (!r.IsMatch(href))
        throw new Exception("Unsupported href format for TT.Video");
      var groups = r.Match(href).Groups;
      this.HandleID = groups[1].ToString();
      this.TTID = Convert.ToUInt64(groups[2].ToString());
    }
    #endregion
    //https://www.tiktok.com/@bellapoarch/video/6951069953248300293?
    public override string HrefEnd => $"@{HandleID}/video/{TTID}";
    public Channel Channel { get; set; } public Guid? ChannelID { get; set; }
    public Handle Handle { get; set; } public string HandleID { get; set; }
    public Audio Audio { get; set; } public Guid? AudioID { get; set; }
    public string Description { get; set; }

    //public VideoInfo Info { get; set; }
    //public ICollection<Comment> Comments { get; set; }
    //public ICollection<Video_Description> Descriptions { get; set; }
  }
}