using Microsoft.AspNetCore.Mvc;
using museum_artifact_manager.Data;
using museum_artifact_manager.Models;
using Microsoft.EntityFrameworkCore;
namespace museum_artifact_manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestorationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RestorationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Restorations.ToList());
        }

        [HttpPost]
        public IActionResult Create(Restoration restoration)
        {
            _context.Restorations.Add(restoration);
            _context.SaveChanges();

            return Ok(restoration);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var restoration = _context.Restorations.Find(id);

            if (restoration == null)
                return NotFound();

            _context.Restorations.Remove(restoration);
            _context.SaveChanges();

            return Ok();
        }
        [HttpPut("{id}")]
public async Task<IActionResult> UpdateRestoration(int id, Restoration restoration)
{
    if (id != restoration.Id)
    {
        return BadRequest();
    }

    _context.Entry(restoration).State = EntityState.Modified;

    await _context.SaveChangesAsync();

    return NoContent();
}
    }
}