using GymScheduling.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymScheduling.Data
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<ClassSession> ClassSessions { get; set; } = null!;
        public DbSet<Scheduling> Schedullings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Scheduling>()
                .HasIndex(b => new { b.StudentId, b.ClassSessionId })
                .IsUnique();

            // Relations (optional explicit)
            modelBuilder.Entity<Scheduling>()
                .HasOne(b => b.Student)
                .WithMany()
                .HasForeignKey(b => b.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Scheduling>()
                .HasOne(b => b.ClassSession)
                .WithMany(s => s.Schedulings)
                .HasForeignKey(b => b.ClassSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
