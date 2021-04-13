using System;
using System.Collections.Generic;
namespace TiktokE.Core
{
  public class UserVideoInteraction : IDed
  {
    public User User { get; set; } public Guid UserID { get; set; }
    public TT.Video Video { get; set; } public Guid VideoID { get; set; }
    public VideoInteraction Type { get; set; }
  }
}
