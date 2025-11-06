using GymScheduling.Domain.Enums;

namespace GymScheduling.DTOs
{
    public class CreateStudentDto
    {
        public string Name { get; set; } = null!;
        public PlanType Plan { get; set; }
    }
}
