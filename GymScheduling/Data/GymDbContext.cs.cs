using Microsoft.EntityFrameworkCore;
using GymScheduling.Domain.Entities;


public class GymDbContext : DbContext
{
    public GymDbContext(DbContextOptions<GymDbContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<ClassSession> ClassSessions { get; set; } = null!;
    public DbSet<Scheduling> Schedulings { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Scheduling>().HasIndex(b => new { b.StudentId, b.ClassSessionId }).IsUnique();
    }
}
