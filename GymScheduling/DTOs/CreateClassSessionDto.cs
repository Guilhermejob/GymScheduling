namespace GymScheduling.Api.DTOs
{
    public class CreateClassSessionDto
    {
        public string ClassType { get; set; } = null!;
        public DateTime StartAt { get; set; }
        public int Capacity { get; set; }
        public string? Location { get; set; }
        public string? Instructor { get; set; }
    }
}
