using GymScheduling.Domain.Enums;

namespace GymScheduling.Domain.Entities
{

    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public PlanType Plan { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}