using System;
using System.Collections.Generic;
namespace TiktokE.Core.TT
{
  public class Video : Timed, ITTIDed<long>
  {
    public string Href { get; set; }
    public Handle Handle { get; set; } public string HandleID { get; set; }
    public long TTID { get; set; }
    
    public Audio Audio { get; set; } public Guid? AudioID { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Video_Description> Descriptions { get; set; }
  }
}