using GymScheduling.Data;
using GymScheduling.Domain.Entities;
using GymScheduling.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymScheduling.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly GymDbContext _db;
        public StudentController(GymDbContext db) => _db = db;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentDto dto)
        {
            var student = new Student { Name  = dto.Name, Plan = dto.Plan };
            _db.Students.Add(student);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var student = await _db.Students.FindAsync(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        //rota de relatorio simples por aluno
        [HttpGet("{id}/report")]
        public async Task<IActionResult> Report(Guid id, int? year, int? month)
        {
            var student = await _db.Students.FindAsync(id);
            if (student == null) return NotFound();

            var now = DateTime.UtcNow;
            var y = year ?? now.Year;
            var m = month ?? now.Month;
            var start = new DateTime(y, m, 1);
            var end = start.AddMonths(1);

            var query = _db.Schedullings
                .Include(s => s.ClassSession)
                .Where(s => s.StudentId == id && s.ClassSession!.StartTime >= start && s.ClassSession.StartTime < end);

            var total = await query.CountAsync();

            var types = await query
                .GroupBy(s => s.ClassSession!.ClassType)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .ToListAsync();

            return Ok(new { Student = student, TotalThisMonth = total, TopTypes = types });
        }
    }
}
