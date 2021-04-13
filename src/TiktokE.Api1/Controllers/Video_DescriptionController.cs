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
  public class Video_DescriptionController : ControllerBase
  {
    private readonly TiktokEContext _context;

    public Video_DescriptionController(TiktokEContext context)
    {
      _context = context;
    }

    // GET: api/Video_Description
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Video_Description>>> GetVideo_Descriptions()
    {
      return await _context.Video_Descriptions.ToListAsync();
    }

    // GET: api/Video_Description/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Video_Description>> GetVideo_Description(Guid id)
    {
      var video_Description = await _context.Video_Descriptions.FindAsync(id);

      if(video_Description == null) {
        return NotFound();
      }

      return video_Description;
    }

    // PUT: api/Video_Description/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutVideo_Description(Guid id, Video_Description video_Description)
    {
      if(id != video_Description.ID) {
        return BadRequest();
      }

      _context.Entry(video_Description).State = EntityState.Modified;

      try {
        await _context.SaveChangesAsync();
      } catch(DbUpdateConcurrencyException) {
        if(!Video_DescriptionExists(id)) {
          return NotFound();
        } else {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Video_Description
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Video_Description>> PostVideo_Description(Video_Description video_Description)
    {
      _context.Video_Descriptions.Add(video_Description);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetVideo_Description", new { id = video_Description.ID }, video_Description);
    }

    // DELETE: api/Video_Description/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVideo_Description(Guid id)
    {
      var video_Description = await _context.Video_Descriptions.FindAsync(id);
      if(video_Description == null) {
        return NotFound();
      }

      _context.Video_Descriptions.Remove(video_Description);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool Video_DescriptionExists(Guid id)
    {
      return _context.Video_Descriptions.Any(e => e.ID == id);
    }
  }
}
