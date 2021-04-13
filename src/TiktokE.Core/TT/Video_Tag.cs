using System;
using System.Collections.Generic;
namespace TiktokE.Core.TT
{
  public class Video_Tag : Timed
  {
    public Video_Description VideoDesc { get; set; } public Guid VideoDescID { get; set; }
    public Tag Tag { get; set; } public Guid TagID { get; set; }
  }
}
