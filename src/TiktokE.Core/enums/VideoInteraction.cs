using System;

namespace TiktokE.Core
{
  [Flags]
  public enum VideoInteraction
  {
    Disliked,
    Seen,
    Liked,
    Commented,
    Saved,
    MarkedToDownload,
    Downloaded
  }
}
