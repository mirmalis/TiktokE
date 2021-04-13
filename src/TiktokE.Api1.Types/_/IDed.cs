using System;

namespace TiktokE.Api1.Types
{
  public class IDed : IIDed<Guid>
  {
    public Guid ID { get; set; }
  }
}
