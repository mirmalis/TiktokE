using System;
using System.Collections.Generic;

namespace TiktokE.Api1.Types.User
{
  
  public interface I : IIDed<Guid>
  {
    public string Name { get; set; }
  }
  public class Deep : I
  {
    public Guid ID { get; set; }
    public string Name { get; set; }
    public object ChannelPreferencesInfo { get; set; }
    public object TagPreferencesInfo { get; set; }
    public object VideoInteractionsInfo { get; set; }
  }
  public class Shallow : I
  {
    public Guid ID { get; set; }
    public string Name { get; set; }
  }
}
