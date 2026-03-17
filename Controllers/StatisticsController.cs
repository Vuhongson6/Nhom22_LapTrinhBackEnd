using Microsoft.AspNetCore.Mvc;
using museum_artifact_manager.Data;
using Microsoft.EntityFrameworkCore;

namespace museum_artifact_manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StatisticsController(AppDbContext context)
        {
            _context = context;
        }

        // Tổng số hiện vật
        [HttpGet("artifact-count")]
        public IActionResult GetArtifactCount()
        {
            var count = _context.Artifacts.Count();
            return Ok(new { TotalArtifacts = count });
        }

        // Tổng số triển lãm
        [HttpGet("exhibition-count")]
        public IActionResult GetExhibitionCount()
        {
            var count = _context.Exhibitions.Count();
            return Ok(new { TotalExhibitions = count });
        }

        // Tổng số nhân viên
        [HttpGet("staff-count")]
        public IActionResult GetStaffCount()
        {
            var count = _context.Staffs.Count();
            return Ok(new { TotalStaff = count });
        }

        // Tổng số phục chế
        [HttpGet("restoration-count")]
        public IActionResult GetRestorationCount()
        {
            var count = _context.Restorations.Count();
            return Ok(new { TotalRestorations = count });
        }
        
        [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboard()
    {
    var artifacts = await _context.Artifacts.CountAsync();
    var exhibitions = await _context.Exhibitions.CountAsync();
    var staff = await _context.Staffs.CountAsync();
    var restorations = await _context.Restorations.CountAsync();

    return Ok(new
    {
        artifacts,
        exhibitions,
        staff,
        restorations
    });
}
    }
}