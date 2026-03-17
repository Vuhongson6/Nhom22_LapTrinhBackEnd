using Microsoft.AspNetCore.Mvc;
using museum_artifact_manager.Data;
using museum_artifact_manager.Models;
using Microsoft.EntityFrameworkCore;
namespace museum_artifact_manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StaffController(AppDbContext context)
        {
            _context = context;
        }

        // GET tất cả nhân viên
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Staffs.ToList());
        }

        // POST thêm nhân viên
        [HttpPost]
        public IActionResult Create(Staff staff)
        {
            _context.Staffs.Add(staff);
            _context.SaveChanges();

            return Ok(staff);
        }

        // DELETE nhân viên
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var staff = _context.Staffs.Find(id);

            if (staff == null)
                return NotFound();

            _context.Staffs.Remove(staff);
            _context.SaveChanges();

            return Ok();
        }
        [HttpPut("{id}")]
public async Task<IActionResult> UpdateStaff(int id, Staff staff)
{
    if (id != staff.Id)
    {
        return BadRequest();
    }

    _context.Entry(staff).State = EntityState.Modified;

    await _context.SaveChangesAsync();

    return NoContent();
}
    }
}