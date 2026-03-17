using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using museum_artifact_manager.Data;
using museum_artifact_manager.Models;

namespace museum_artifact_manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtifactController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ArtifactController(AppDbContext context)
        {
            _context = context;
        }

        // GET all artifacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artifact>>> GetArtifacts()
        {
            return await _context.Artifacts.ToListAsync();
        }

        // GET artifact by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Artifact>> GetArtifact(int id)
        {
            var artifact = await _context.Artifacts.FindAsync(id);

            if (artifact == null)
            {
                return NotFound();
            }

            return artifact;
        }

        // JOIN Artifact + Exhibition
        [HttpGet("details")]
        public async Task<ActionResult> GetArtifactsWithExhibition()
        {
            var result = await _context.Artifacts
                .Include(static a => a.Exhibition)
                .Select(static a => new
                {
                    a.Id,
                    a.Name,
                    a.Description,
                    a.Location,
                    a.Status,
                    ExhibitionName = a.Exhibition!.Name
                })
                .ToListAsync();

            return Ok(result);
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<Artifact>> CreateArtifact(Artifact artifact)
        {
            _context.Artifacts.Add(artifact);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetArtifact), new { id = artifact.Id }, artifact);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtifact(int id)
        {
            var artifact = await _context.Artifacts.FindAsync(id);
            if (artifact == null)
            {
                return NotFound();
            }
            _context.Artifacts.Remove(artifact);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
public async Task<IActionResult> UpdateArtifact(int id, Artifact artifact)
{
    if (id != artifact.Id)
    {
        return BadRequest();
    }

    _context.Entry(artifact).State = EntityState.Modified;

    await _context.SaveChangesAsync();

    return NoContent();
}
    }
    
}