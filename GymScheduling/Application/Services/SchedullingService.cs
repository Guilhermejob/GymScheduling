using GymScheduling.Application.Results;
using GymScheduling.Data;
using GymScheduling.Domain.Entities;
using GymScheduling.Domain.Enums;
using Microsoft.EntityFrameworkCore;


namespace GymScheduling.Application.Services
{
    public class SchedulingService
    {
        private readonly GymDbContext _db;

        public SchedulingService(GymDbContext db)
        {
            _db = db;
        }

        public async Task<Result> ScheduleStudentAsync(Guid studentId, Guid classSessionId)
        {
            using var transaction = await _db.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
            try
            {
                var session = await _db.ClassSessions
                    .Include(cs => cs.Schedulings)
                    .FirstOrDefaultAsync(cs => cs.Id == classSessionId);
                if (session == null) return Result.Failure("Aula não encontrada");

                var student = await _db.Students.FindAsync(studentId);
                if (student == null) return Result.Failure("Aluno não encontrado");

                var existing = await _db.Schedullings.AnyAsync(s => s.StudentId == studentId && s.ClassSessionId == classSessionId);
                if (existing) return Result.Failure("Aluno já está inscrito nesta aula");

                var currentCount = await _db.Schedullings.CountAsync(s => s.ClassSessionId == classSessionId);
                if (currentCount >= session.Capacity) return Result.Failure("Aula está cheia");

                var monthStart = new DateTime(session.StartTime.Year, session.StartTime.Month, 1);
                var monthEnd = monthStart.AddMonths(1);
                var schedulesThisMonth = await _db.Schedullings
                    .Include(s => s.ClassSession)
                    .CountAsync(s => s.StudentId == studentId && s.ClassSession.StartTime >= monthStart && s.ClassSession.StartTime < monthEnd);

                var limit = student.Plan switch
                {
                    PlanType.Mensal => 12,
                    PlanType.Trimestral => 20,
                    PlanType.Anual => 30,
                    _ => 12
                };

                if (schedulesThisMonth >= limit) return Result.Failure("Limite mensal de aulas atingido para o plano do aluno");

                var scheduling = new Scheduling { Id = Guid.NewGuid(), StudentId = studentId, ClassSessionId = classSessionId };
                _db.Schedullings.Add(scheduling);

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                return Result.Success();

            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        } 

    } 
} 


