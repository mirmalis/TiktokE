using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiktokE.Api1.Types.Channel
{
  namespace Post
  {
    public class TTID_Handle_Name
    {
      public string TTID { get; set; }
      public string Handle { get; set; }
      public string Name { get; set; }
    }
  }
  public interface I : IIDed<Guid>
  {
    public string TTID { get; set; }
    public string Handle { get; set; }
  }
  public class ChannelDeep : I
  {
    public Guid ID { get; set; }
    public string TTID { get; set; }
    public string Handle { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; }
  }
  public class ChannelShallow : I
  {
    public Guid ID { get; set; }
    public string TTID { get; set; }
    public string Handle { get; set; }
  }
}
