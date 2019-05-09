using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRatings.Models;

namespace MovieRatings.Controllers.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaHousesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MediaHousesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/MediaHouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MediaHouse>>> GetMediaHouses()
        {
            return await _context.MediaHouses.ToListAsync();
        }

        // GET: api/MediaHouses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MediaHouse>> GetMediaHouse(int id)
        {
            var mediaHouse = await _context.MediaHouses.FindAsync(id);

            if (mediaHouse == null)
            {
                return NotFound();
            }

            return mediaHouse;
        }

        // PUT: api/MediaHouses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMediaHouse(int id, MediaHouse mediaHouse)
        {
            if (id != mediaHouse.MediaHouseId)
            {
                return BadRequest();
            }

            _context.Entry(mediaHouse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MediaHouseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMediaHouse", new { id = mediaHouse.MediaHouseId }, mediaHouse);
        }

        // POST: api/MediaHouses
        [HttpPost]
        public async Task<ActionResult<MediaHouse>> PostMediaHouse(MediaHouse mediaHouse)
        {
            _context.MediaHouses.Add(mediaHouse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMediaHouse", new { id = mediaHouse.MediaHouseId }, mediaHouse);
        }

        // DELETE: api/MediaHouses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MediaHouse>> DeleteMediaHouse(int id)
        {
            var mediaHouse = await _context.MediaHouses.FindAsync(id);
            if (mediaHouse == null)
            {
                return NotFound();
            }

            _context.MediaHouses.Remove(mediaHouse);
            await _context.SaveChangesAsync();

            return mediaHouse;
        }

        private bool MediaHouseExists(int id)
        {
            return _context.MediaHouses.Any(e => e.MediaHouseId == id);
        }
    }
}
