using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiktokE.Api1.Types.Video
{
  namespace Get
  {
    public interface I : IIDed<Guid>
    {
      public string Handle { get; set; }
      public string TTID { get; set; }
    }
    public class VideoDeep : I
    {
      public Guid ID { get; set; }
      public string Handle { get; set; }
      public string TTID { get; set; }
    }
    public class VideoShallow : I
    {
      public Guid ID { get; set; }
      public string Handle { get; set; }
      public string TTID { get; set; }
    }
  }
  namespace Post
  {
    public class TTID_Channel_Description
    {
      public class ChannelInfo
      {
        public string Name { get; set; }
        public string TTID { get; set; }
        public string Handle { get; set; }
        public bool? Verified { get; set; }
      }
      public class AudioInfo
      {
        public string Name { get; set; }
        public string TTID { get; set; }
      }
      public string TTID { get; set; }
      public ChannelInfo Channel { get; set; }
      public AudioInfo Audio { get; set; }
      public string Description { get; set; }
      public string Src { get; set; }
    }
  }
}
