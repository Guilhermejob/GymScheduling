namespace GymScheduling.Domain.Entities;

public class Scheduling
{

    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Student Student { get; set; } = null!;
    public Guid ClassSessionId { get; set; }
    public ClassSession ClassSession { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}
