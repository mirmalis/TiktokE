using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TiktokE.Seed.Reimplementations
{
  public class User : Core.User
  {
    #region Constructors
    public User() { }
    public User(string name) : base()
    {
      this.Name = name;

    }
    #endregion
    public User AddUplaoderPreference(Core.PreferenceType type, params Core.TT.Channel[] channels)
    {
      this.ChannelPreferencess ??= new List<Core.UploaderPreference>();
      foreach (var preference in channels.Select(channel => new Core.UploaderPreference(){ User = this, Channel = channel, Type = type })) { 
        this.ChannelPreferencess.Add(preference);
      }
      return this;
    }
    //public User AddTagPreference(Core.PreferenceType type, params Core.TT.Tag[] linkTexts)
    //{
    //  this.TagPreferences ??= new List<Core.TagPreference>();

    //  foreach (var preference in linkTexts.Select(item => new Core.TagPreference(){ User = this, Tag = item, Type = type }))
    //  {
    //    this.TagPreferences.Add(preference);
    //  }

    //  return this;
    //}
    public User AddVideoInterraction(Core.VideoInteraction type, params Video[] videos)
    {
      this.VideoInteractions ??= new List<Core.UserVideoInteraction>();

      foreach(var item in videos.Select(video => new Core.UserVideoInteraction() { User = this, Video = video, Type = type })) {
        this.VideoInteractions.Add(item);
      }

      return this;
    }
  }
}
