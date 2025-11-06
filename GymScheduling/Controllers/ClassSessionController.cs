using GymScheduling.Api.DTOs;
using GymScheduling.Data;
using GymScheduling.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace GymScheduling.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ClassSessionController : ControllerBase
    {

        private readonly GymDbContext _db;
        public ClassSessionController(GymDbContext db) => _db = db;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClassSessionDto dto)
        {


            bool locationConflict = await _db.ClassSessions.AnyAsync(cs =>
            cs.Location == dto.Location &&
                cs.StartTime == dto.StartAt
            );
            if (locationConflict) return BadRequest(new { message = "Já existe uma aula Agendada neste local e horário" });

            if (dto.StartAt < DateTime.UtcNow)
            {
                return BadRequest(new
                {
                    message = "Não é possível criar uma aula em um horário passado."
                });
            }

            var session = new ClassSession
            {
                ClassType = dto.ClassType,
                StartTime = dto.StartAt,
                Capacity = dto.Capacity,
                Location = dto.Location,
                Instructor = dto.Instructor
            };

            _db.ClassSessions.Add(session);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = session.Id }, session);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var session = await _db.ClassSessions
                .Include(s => s.Schedulings)  
                .ThenInclude(b => b.Student)            
                .FirstOrDefaultAsync(s => s.Id == id);

            if (session == null) return NotFound();
            return Ok(session);
        }


        [HttpGet]
        public async Task<IActionResult> ListUpcoming()
        {
            var now = DateTime.UtcNow;
            var list = await _db.ClassSessions
                .Where(cs => cs.StartTime >= now)
                .OrderBy(cs => cs.StartTime)
                .ToListAsync();

            return Ok(list);
        }

    }
}
