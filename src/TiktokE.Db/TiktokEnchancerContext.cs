using Microsoft.EntityFrameworkCore;
using System;

namespace TiktokE.Db
{
  public class TiktokEContext : DbContext
  {
    public DbSet<Core.User> Users { get; set; }
    public DbSet<Core.UserVideoInteraction> UserVideoInteractions { get; set; }
    public DbSet<Core.UploaderPreference> UploaderPreferences { get; set; }
    public DbSet<Core.TagPreference> TagPreferences { get; set; }

    public DbSet<Core.TT.Video> Videos { get; set; }
    public DbSet<Core.TT.Video_Description> Video_Descriptions { get; set; }
    public DbSet<Core.TT.Tag> Tags { get; set; }

    public DbSet<Core.TT.Channel> Channels { get; set; }
    public DbSet<Core.TT.Handle> Handles { get; set; }
    public DbSet<Core.TT.Comment> Comments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
      optionsBuilder.UseSqlite($"Data Source=D:/dbs/TiktokE.db3");
      //optionsBuilder.UseMySQL("Server=localhost;Database=datar;User=datarApp;Password=v5p2bd3tm5Z3XamZB7vhxabgutMBqAaaasd;");
    }
    protected override void OnModelCreating(ModelBuilder mb)
    {
      base.OnModelCreating(mb);
      // HasAlternateKey // isUnique
      
      mb.Entity<Core.TT.Video>().HasAlternateKey(item => new { item.TTID, item.HandleID });
      mb.Entity<Core.TT.Video>().HasAlternateKey(item => item.TTID);
      mb.Entity<Core.UploaderPreference>().HasAlternateKey(item => new { item.UserID, item.ChannelID });
      mb.Entity<Core.TagPreference>().HasAlternateKey(item => new { item.UserID, item.TagID });
      // HasKey
      // Relations
      // Property
      // Owns
      // ToTable
    }
  }
}
