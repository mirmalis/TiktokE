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
  public class ChannelsController : ControllerBase
  {
    private readonly TiktokEContext _context;

    public ChannelsController(TiktokEContext context)
    {
      _context = context;
    }

    #region GET
    // GET: api/Channels
    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<Types.Channel.ChannelShallow>>> GetChannels()
    //{
    //  return await Helpers.Channel.Shallow.Includes(_context.Channels)
    //    .Select(item => new Helpers.Channel.Shallow(null, item))
    //    .ToListAsync()
    //  ;
    //}

    // GET: api/Channels/E8A2311F-04E6-4FF5-9B57-F5FAC1F83B44
    //[HttpGet("{id:guid}")]
    //public async Task<ActionResult<Types.Channel.ChannelDeep>> GetChannel(Guid id)
    //{
    //  var channel = await Helpers.Channel.Deep.Includes(_context.Channels)
    //    .FirstOrDefaultAsync(item => item.ID == id);

    //  if (channel == null)
    //  {
    //    return NotFound();
    //  }

    //  return new Helpers.Channel.Deep(null, channel);
    //}

    #region "/api/channels{id:guid}"
    // GET: api/channel/E8A2311F-04E6-4FF5-9B57-F5FAC1F83B44
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Types.Channel.ChannelDeep>> GetChannel(Guid id)
    {
      var channel = await Helpers.Channel.Deep.Includes(_context.Channels)
        .FirstOrDefaultAsync(item => item.ID == id);
      if (channel == null)
        return NotFound();
      return new Helpers.Channel.Deep(null, channel);
    }
    #endregion
    #region "/api/channels?{ttID, handleID}"
    // GET: api/Channels?ttID=123123123123
    // GET: api/Channels?handleID=elllas69
    [HttpGet("")]
    public async Task<ActionResult<Types.Channel.ChannelDeep>> GetChannel(string ttID = null, string handleID = null)
    {
      var q = Helpers.Channel.Deep.Includes(_context.Channels);
      Core.TT.Channel channel;

      if (UInt64.TryParse(ttID, out ulong ttid))
        channel = await q.FirstOrDefaultAsync(item => item.TTID == ttid);
      else if (handleID != null)
        channel = await q.FirstOrDefaultAsync(item => item.ActiveHandleID == handleID);
      else
        return this.BadRequest();

      if (channel == null)
        return NotFound();

      return new Helpers.Channel.Deep(null, channel);
    }  
    #endregion
    #endregion
    #region PUT
    // PUT: api/Channels/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutChannel(Guid id, Channel channel)
    {
      if (id != channel.ID)
      {
        return BadRequest();
      }

      _context.Entry(channel).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ChannelExists(id))
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
    // POST: api/Channels
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Types.Channel.ChannelShallow>> PostChannel(Helpers.Channel.Post.TTID_Handle_Name channelInfo)
    {
      if (!ulong.TryParse(channelInfo.TTID, out ulong ttid))
      {
        return BadRequest("Bad TTID");
      }
      var channel = await _context.Channels.FirstOrDefaultAsync(item => item.TTID == ttid);

      if (channel != null)
      {
        _context.Entry(channel).State = EntityState.Modified;
        channel.LastSeen = DateTime.Now;
        if (channel.Name != channelInfo.Name)
        {
          channel.Name = channelInfo.Name;
        }
        if (channel.ActiveHandleID != channelInfo.Handle)
        {
          channel.ActiveHandleID = channelInfo.Handle;
          if (!_context.Handles.Any(item => item.ID == channelInfo.Handle))
          {
            channel.ActiveHandle = new Handle(){
              ID = channelInfo.Handle
            };
          }
        }

        await _context.SaveChangesAsync();
        return CreatedAtAction("GetChannel", new { id = channel.ID }, new Helpers.Channel.Shallow(null, channel));
      }
      else // Jei reikia sukurti naują
      {
        if (!_context.Handles.Any(item => item.ID == channelInfo.Handle))
        {
          var handle = new Core.TT.Handle(){
            ID = channelInfo.Handle
          };
          _context.Handles.Add(handle);
        }
        channel = channelInfo.Item;
        _context.Channels.Add(channel);
      }
      await _context.SaveChangesAsync();
      return CreatedAtAction("GetChannel", new { id = channel.ID }, new Helpers.Channel.Shallow(null, channel));

    }
    #endregion
    #region DELETE
    // DELETE: api/Channels/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteChannel(Guid id)
    {
      var channel = await _context.Channels.FindAsync(id);
      if (channel == null)
      {
        return NotFound();
      }

      _context.Channels.Remove(channel);
      await _context.SaveChangesAsync();

      return NoContent();
    } 
    #endregion

    private bool ChannelExists(Guid id)
    {
      return _context.Channels.Any(e => e.ID == id);
    }
  }
}
