using System;
using System.Collections.Generic;
using System.Text;

namespace TiktokE.Core.TT
{
  public class Description_Tag : IDed
  {
    public Video_Description Description { get; set; } public Guid DescriptionID { get; set; }
    public Tag Tag { get; set; } public string TagID { get; set; }
  }
}
