using System;
using System.Collections.Generic;
namespace TiktokE.Core.TT
{
  public class Tag : IIDed<string>
  {
    public string ID { get; set; }


    public ICollection<Description_Tag> Assignments { get; set; }
  }
}