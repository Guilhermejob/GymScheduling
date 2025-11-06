namespace GymScheduling.DTOs
{
    public class CreateScheduleDto
    {
        public Guid StudentId { get; set; }
        public Guid ClassSessionId { get; set; }
    }
}
