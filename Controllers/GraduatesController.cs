using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraduatesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GraduatesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Graduates
        [HttpGet("GetGraduates")]
        public async Task<ActionResult<IEnumerable<Graduate>>> GetGraduates()
        {
            return await _context.Graduates.ToListAsync();
        }

        // GET: api/Graduates/5
        [HttpGet("GetGraduate/{id}")]
        public async Task<ActionResult<Graduate>> GetGraduate(Guid id)
        {
            var graduate = await _context.Graduates.FindAsync(id);

            if (graduate == null)
            {
                return NotFound();
            }

            return graduate;
        }

        // PUT: api/Graduates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutGraduate/{id}")]
        public async Task<IActionResult> PutGraduate(Guid id, Graduate graduate)
        {
            if (id != graduate.GraduateId)
            {
                return BadRequest();
            }

            graduate.DateEdited = DateTime.UtcNow;

            _context.Entry(graduate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GraduateExists(id))
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

        // POST: api/Graduates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostGraduate")]
        public async Task<ActionResult<Graduate>> PostGraduate(Graduate graduate)
        {
            _context.Graduates.Add(graduate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGraduate", new { id = graduate.GraduateId }, graduate);
        }

        // DELETE: api/Graduates/5
        [HttpDelete("DeleteGraduate/{id}")]
        public async Task<IActionResult> DeleteGraduate(Guid id)
        {
            var graduate = await _context.Graduates.FindAsync(id);
            if (graduate == null)
            {
                return NotFound();
            }

            _context.Graduates.Remove(graduate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GraduateExists(Guid id)
        {
            return _context.Graduates.Any(e => e.GraduateId == id);
        }
    }
}
