using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiktokE.Core;
using TiktokE.Db;

namespace TiktokE.Api1.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    #region Constructors
    private readonly TiktokEContext _context;
    public UsersController(TiktokEContext context)
    {
      _context = context;
    } 
    #endregion
    #region User
    //// GET: api/Users
    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<Types.User.Shallow>>> GetUsers()
    //{
    //  return await Helpers.User.Shallow.Includes(_context.Users)
    //    .Select(item => new Helpers.User.Shallow(null, item))
    //    .ToListAsync()
    //  ;
    //}

    // GET: api/Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Types.User.Deep>> GetUser(Guid id)
    {
      var user = await Helpers.User.Deep.Includes(_context.Users)
        .FirstOrDefaultAsync(item => item.ID == id)
      ;

      if (user == null)
      {
        return NotFound();
      }

      return new Helpers.User.Deep(null, user);
    }

    // PUT: api/Users/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(Guid id, User user)
    {
      if (id != user.ID)
      {
        return BadRequest();
      }

      _context.Entry(user).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!UserExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Users
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetUser", new { id = user.ID }, user);
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
      var user = await _context.Users.FindAsync(id);
      if (user == null)
      {
        return NotFound();
      }

      _context.Users.Remove(user);
      await _context.SaveChangesAsync();

      return NoContent();
    } 
    #endregion
    #region tagPreferences
    //// GET: api/Users/5/tagpreferences
    //[HttpGet("{id}/tagpreferences")]
    //public async Task<ActionResult<IEnumerable<Types.User.XTagPreference>>> GetUserTagPreferences(Guid id)
    //{
    //  if (!UserExists(id))
    //  {
    //    return NotFound();
    //  }
    //  return await _context.TagPreferences
    //    .Where(item => item.UserID == id)
    //    .Select(item => new Types.User.XTagPreference()
    //    {
    //      ID = item.ID,
    //      TagID = item.TagID,
    //      Delete = item.Type == PreferenceType.Ignore
    //    })
    //    .ToListAsync()
    //  ;
    //}
    #endregion
    #region deletedLinks
    //[HttpGet("{id}/deletedlinks")]
    //public async Task<ActionResult<IEnumerable<string>>> GetDeletedTags(Guid id)
    //{
    //  if (!UserExists(id))
    //  {
    //    return NotFound();
    //  }
    //  return await _context.TagPreferences
    //    .Where(item => item.UserID == id && item.Type == PreferenceType.Ignore)
    //    .Select(item => $"#{item.TagID}")
    //    .ToListAsync()
    //  ;
    //}
    #endregion
    #region deletedUploaders
    [HttpGet("{id}/deletedUploaders")] public async Task<ActionResult<IEnumerable<string>>> GetDeletedUploaders(Guid id)
    {
      if (!UserExists(id))
      {
        return NotFound();
      }
      return await _context.UploaderPreferences
        .Include(item => item.Channel).ThenInclude(item => item.Handles)
        .Where(item => item.UserID == id && item.Type == PreferenceType.Ignore)
        .SelectMany(item => item.Channel.Handles.Select(item => item.Handle.ID))
        .ToListAsync()
      ;
    }
    [HttpDelete("{id}/deletedUploaders/{handleName}")] public async Task<ActionResult<IEnumerable<string>>> DeleteDeletedUploaders(Guid id, string handleName)
    {
      if (!UserExists(id))
        return NotFound();
      var handle = _context.Handles
        .Include(item => item.ChannelAssignments)
        .FirstOrDefault(item => item.ID == handleName);
      if (handle == null)
        return BadRequest("No such handle in a database");

      var channelIDs = handle.ChannelAssignments.Where(item => item.Until == null).Select(item => item.ChannelID);
      var preferences = _context.UploaderPreferences.Where(item => item.UserID == id && channelIDs.Contains(item.ChannelID));
      _context.UploaderPreferences.RemoveRange(preferences);
      await _context.SaveChangesAsync();
      return NoContent();
    }
    [HttpPost("{id}/deletedUploaders")] public async Task<ActionResult> PostDeletedUploaders(Guid id, string handleName)
    {
      if (!HandleExists(handleName))
      {
        _context.Channels.Add(
          new Core.TT.Channel()
          {
            Name = handleName,
            Handles = new List<Core.TT.Channel_Handle>() {
              new Core.TT.Channel_Handle(){
                Handle = new Core.TT.Handle() {
                  ID = handleName
                }
              }
            }
          }
        );
        await _context.SaveChangesAsync();
      }
      var usersUploaderPreferences = await _context.UploaderPreferences.Where(item => item.UserID == id).ToListAsync();
      var handle = _context.Handles
        .Include(item => item.ChannelAssignments)
        .FirstOrDefault(item => item.ID == handleName)
      ;
      var channelsToBlock = handle
        ?.ChannelAssignments
        .Where(item => item.IsActive(DateTime.Now))
      ;
      foreach (var channel in channelsToBlock)
      {
        var existing = usersUploaderPreferences.FirstOrDefault(item => item.UserID == id && item.ChannelID == channel.ChannelID);
        if(existing == null)
        {
          _context.UploaderPreferences.Add(new Core.UploaderPreference()
          {
            ChannelID = channel.ChannelID,
            UserID = id,
            Type = PreferenceType.Ignore
          });
        } else
        {
          if (existing.Type != PreferenceType.Ignore)
          {
            _context.Entry(existing).State = EntityState.Modified;
          }
        }
      }
      await _context.SaveChangesAsync();
      return NoContent();
    }
    #endregion
    #region Privates
    private bool UserExists(Guid id)
    {
      return _context.Users.Any(e => e.ID == id);
    }
    private bool HandleExists(string id)
    {
      return _context.Handles.Any(e => e.ID == id);
    } 
    #endregion
  }
}
