using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using museum_artifact_manager.Data;
using museum_artifact_manager.Models;

namespace museum_artifact_manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExhibitionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExhibitionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/exhibition
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exhibition>>> GetExhibitions()
        {
            return await _context.Exhibitions.ToListAsync();
        }

        // GET: api/exhibition/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Exhibition>> GetExhibition(int id)
        {
            var exhibition = await _context.Exhibitions.FindAsync(id);

            if (exhibition == null)
            {
                return NotFound();
            }

            return exhibition;
        }

        // POST: api/exhibition
        [HttpPost]
        public async Task<ActionResult<Exhibition>> CreateExhibition(Exhibition exhibition)
        {
            _context.Exhibitions.Add(exhibition);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExhibition), new { id = exhibition.Id }, exhibition);
        }

        // PUT: api/exhibition/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExhibition(int id, Exhibition exhibition)
        {
            if (id != exhibition.Id)
            {
                return BadRequest();
            }

            _context.Entry(exhibition).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/exhibition/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExhibition(int id)
        {
            var exhibition = await _context.Exhibitions.FindAsync(id);

            if (exhibition == null)
            {
                return NotFound();
            }

            _context.Exhibitions.Remove(exhibition);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}