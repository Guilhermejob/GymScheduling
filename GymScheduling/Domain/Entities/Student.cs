namespace GymScheduling.Domain.Entities
{
    public enum PlanType { Mensal = 1, Trimestral = 2, Anual = 3 }

    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public PlanType Plan { get; set; }
    }
}