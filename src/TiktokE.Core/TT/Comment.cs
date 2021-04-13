using System;
using System.Collections.Generic;
using System.Text;

namespace TiktokE.Core.TT
{
  public class Comment : Timed
  {
    public Video Video { get; set; } public Guid VideoID { get; set; }
    public Channel Author { get; set; } public Guid AuthorID { get; set; }
    public string Text { get; set; }

    public Comment ReplyTo { get; set; } public Guid? ReplyToID { get; set; }
  }
}
