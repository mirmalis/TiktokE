using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiktokE.Core.TT;
using TiktokE.Db;

namespace TiktokE.Api1.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VideosController : ControllerBase
  {
    #region Constructors
    private readonly TiktokEContext _context;
    public VideosController(TiktokEContext context)
    {
      _context = context;
    }
    #region private
    private bool VideoExists(Guid id)
    {
      return _context.Videos.Any(e => e.ID == id);
    }
    #endregion
    #endregion
    #region GET
    #region "/api/videos/{id:guid}"
    // GET: api/Videos/5
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Types.Video.Get.VideoDeep>> GetVideo(Guid id)
    {
      var video = await Helpers.Video.Get.Deep.Includes(_context.Videos).FirstOrDefaultAsync(item => item.ID == id);
      if (video == null)
      {
        return NotFound();
      }
      return new Helpers.Video.Get.Deep(null, video);
    }
    #endregion
    #region "/api/videos?{channelhandle,TTID}"
    // GET: api/Videos?channelhandle=elllas69&TTID=123123123123123
    // GET: api/Videos?channelhandle=elllas69
    // GET: api/Videos?TTID=123123123123123
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<Types.Video.Get.VideoShallow>>> GetVideo(string channelhandle, string TTID)
    {
      bool noTTID = false;
      bool noHandle = string.IsNullOrEmpty(channelhandle);
      if (!ulong.TryParse(TTID, out ulong ttid))
      {
        noTTID = true;
      }
      if (noTTID && noHandle)
      {
        return BadRequest("Naither suitable TTID nor channelhandle provided.");
      }
      return await Helpers.Video.Get.Shallow.Includes(_context.Videos)
        .Where(item =>
          (noTTID || item.TTID == ttid) &&
          (noHandle || item.HandleID == channelhandle)
        )
        .Select(item => new Helpers.Video.Get.Shallow(null, item))
        .ToListAsync()
      ;
    }
    #endregion
    #endregion
    #region PUT
    // PUT: api/Videos/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutVideo(Guid id, Video video)
    {
      if (id != video.ID)
      {
        return BadRequest();
      }

      _context.Entry(video).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!VideoExists(id))
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
    #endregion
    #region POST
    // POST: api/Videos
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Types.Video.Get.VideoShallow>> PostVideo(Types.Video.Post.TTID_Channel_Description data)
    {
      if (!ulong.TryParse(data.TTID, out ulong videoTTID))
      {
        return BadRequest("Invalid TTID for video");
      }


      #region Channel
      Core.TT.Channel channel;
      if (ulong.TryParse(data.Channel.TTID, out ulong channelTTID))
      {
        channel = _context.Channels.FirstOrDefault(item => item.TTID == channelTTID);
        if (channel != null) // If channel already exists in database
        {
          _context.Entry(channel).State = EntityState.Modified;
          if (channel.ActiveHandleID == data.Channel.Handle && channel.TTID == channelTTID) // same core data
          {
          }
          else // dieferent core ddata of a channel
          {
            if (!_context.Handles.Any(item => item.ID == data.Channel.Handle))
            {
              _context.Handles.Add(new Core.TT.Handle(data.Channel.Handle));
            }
            channel.NeedsChecking = true;
          }
          channel.LastSeen = DateTime.Now;
          channel.Verified = data.Channel.Verified;
          channel.ActiveHandleID = data.Channel.Handle;
          channel.Name = data.Channel.Name;
        }
        else
        {
          channel = new Core.TT.Channel(data.Channel.Name,
            channelTTID,
            _context.Handles.FirstOrDefault(item => item.ID == data.Channel.Handle) ?? new Core.TT.Handle(data.Channel.Handle)
          )
          {
            Verified = data.Channel.Verified
          };
        }
      }
      else
      {
        channel = _context.Channels.FirstOrDefault(item => item.ActiveHandleID == data.Channel.Handle)
          ?? throw new Exception("Not enough info about channel")
        ;
      }
      #endregion
      #region Audio
      Core.TT.Audio audio;
      if (ulong.TryParse(data.Audio.TTID, out ulong audioTTID) && !string.IsNullOrEmpty(data.Audio.Name))
      {
        audio = await _context.Audios.FirstOrDefaultAsync(item => item.TTID == audioTTID && item.Name == data.Audio.Name)
          ?? new Core.TT.Audio(audioTTID, data.Audio.Name);
      }
      else
      {
        audio = null;
      }
      #endregion


      var video = _context.Videos
        .FirstOrDefault(item => item.TTID == videoTTID && item.HandleID == channel.ActiveHandleID);
      if (video != null)
      {
        _context.Entry(video).State = EntityState.Modified;
        video.LastSeen = DateTime.Now;
        video.Channel = channel;
        video.HandleID = channel.ActiveHandleID;
        if (video.AudioID != audio.ID)
        {
          video.Audio = audio;
        }
      }
      else
      {
        video = Helpers.Video.Post.Core(data);
        video.Channel = channel;
        video.HandleID = channel.ActiveHandleID;
        video.Audio = audio;
        _context.Videos.Add(video);
      }
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetVideo", new { id = video.ID }, new Helpers.Video.Get.Deep(null, video));
    }
    #endregion
    #region DELETE
    // DELETE: api/Videos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVideo(Guid id)
    {
      var video = await _context.Videos.FindAsync(id);
      if (video == null)
      {
        return NotFound();
      }

      _context.Videos.Remove(video);
      await _context.SaveChangesAsync();

      return NoContent();
    }
    #endregion
  }
}
