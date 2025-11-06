namespace GymScheduling.Domain.Entities;

public class ClassSession
{

    public Guid Id { get; set; }
    public string ClassType { get; set; } = null!; //Ex: Yoga, Pilates, Spinning
    public DateTime StartTime { get; set; } // data e hora que vai começar a aula
    public int Capacity { get; set; } // capacidade máxima de alunos na aula
    public string? Location { get; set; } // Local onde a aula será realizada
    public string? Instructor { get; set; } // Nome do instrutor da aula

    public ICollection<Scheduling> Schedulings { get; set; } = new List<Scheduling>();

}
