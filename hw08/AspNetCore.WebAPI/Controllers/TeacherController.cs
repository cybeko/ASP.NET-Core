using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCore.WebAPI.Models;

namespace AspNetCore.WebAPI.Controllers
{
    [ApiController]
    [Route("api/Teachers")]
    public class TeachersController : ControllerBase
    {
        private readonly TeacherContext _context;

        public TeachersController(TeacherContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            return await _context.Teachers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            var teacher = await _context.Teachers.SingleOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }
            return new ObjectResult(teacher);
        }

        [HttpPut]
        public async Task<ActionResult<Teacher>> PutTeacher(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_context.Teachers.Any(e => e.Id == teacher.Id))
            {
                return NotFound();
            }

            _context.Update(teacher);
            await _context.SaveChangesAsync();
            return Ok(teacher);
        }

        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return Ok(teacher);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Teacher>> DeleteTeacher(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teacher = await _context.Teachers.SingleOrDefaultAsync(m => m.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return Ok(teacher);
        }
    }
}
