using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiktokE.Core.TT
{
  public abstract class TikTokEntity : IDed
  {
    #region Constructors
    public TikTokEntity() { }
    public TikTokEntity(ulong ttid) : base()
    {
      this.TTID = ttid;
    }
    #endregion
    public ulong TTID { get; set; }
    #region Since
    // https://dfir.blog/tinkering-with-tiktok-timestamps/ or https://dfir.pubpub.org/pub/9llea7yp/release/1
    /// <summary>
    /// Returns Creation <see cref="DateTime"/> of entity.
    /// </summary>
    /// <remarks>
    /// First 32 bytes of <see cref="TTID"/> is a Unix timestamp of it created.
    /// </remarks>
    public DateTime Since => new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(Convert.ToInt64(this.TTID) >> 32).ToLocalTime();
    #endregion
    static readonly string Domain = "https://www.tiktok.com/";
    public string Href => Domain + HrefEnd;
    public abstract string HrefEnd { get; }
  }
}