using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiktokE.Api1.Helpers
{
  static class Calc
  {
    internal static string ID(string id, Core.IIDed<string> core)
    {
      if(core?.ID != null && id != null && core.ID != id)
        throw new Exception("Core.ID does not match id, there must be an error");
      return core?.ID ?? id ?? throw new Exception("Provide ID");
    }
    internal static Guid ID(Guid? id, Core.IIDed<Guid> core)
    {
      if(core?.ID != null && id != null && core.ID != id)
        throw new Exception("Core.ID does not match id, there must be an error");
      return core?.ID ?? id ?? throw new Exception("Provide ID");
    }
    internal static ulong TikTokID(DateTime dt, uint rand = 0)
    {
      return (Convert.ToUInt64((dt - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds) << 32) + rand;
    }
  }
}
