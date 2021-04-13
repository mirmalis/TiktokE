using System;
using System.Collections.Generic;

namespace TiktokE.Seed
{
  internal class DataStructure
  {
    readonly List<Core.User> users = new();
    public void AddRange(params Core.User[] elements) => users.AddRange(elements);

    readonly List<Core.TT.Channel> channels = new();
    public void AddRange(params Core.TT.Channel[] elements) => channels.AddRange(elements);
    public void Inject(Db.TiktokEContext context, bool ensureDeleted = false)
    {
      if (ensureDeleted)
        context.Database.EnsureDeleted();
      context.Database.EnsureCreated();

      context.Users.AddRange(users);
      context.Channels.AddRange(channels);

      context.SaveChanges();
    }
  }
}
