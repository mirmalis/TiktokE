using System;
using System.Collections.Generic;
namespace TiktokE.Core
{
  public interface ITTIDed<TKey>
  {
    public TKey TTID { get; set; }
  }
}
