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

    // GET: api/Channels
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Channel>>> GetChannels()
    {
      return await _context.Channels
        .Include(item => item.Handles)
        .ToListAsync();
    }

    // GET: api/Channels/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Channel>> GetChannel(Guid id)
    {
      var channel = await _context.Channels.FindAsync(id);

      if(channel == null) {
        return NotFound();
      }

      return channel;
    }

    // PUT: api/Channels/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutChannel(Guid id, Channel channel)
    {
      if(id != channel.ID) {
        return BadRequest();
      }

      _context.Entry(channel).State = EntityState.Modified;

      try {
        await _context.SaveChangesAsync();
      } catch(DbUpdateConcurrencyException) {
        if(!ChannelExists(id)) {
          return NotFound();
        } else {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Channels
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Channel>> PostChannel(Channel channel)
    {
      _context.Channels.Add(channel);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetChannel", new { id = channel.ID }, channel);
    }

    // DELETE: api/Channels/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteChannel(Guid id)
    {
      var channel = await _context.Channels.FindAsync(id);
      if(channel == null) {
        return NotFound();
      }

      _context.Channels.Remove(channel);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool ChannelExists(Guid id)
    {
      return _context.Channels.Any(e => e.ID == id);
    }
  }
}
