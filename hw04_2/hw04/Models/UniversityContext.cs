using Microsoft.EntityFrameworkCore;

namespace UniversityMVC.Models
{
    public class UniversityContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectGroup> SubjectsGroups { get; set; }


        public UniversityContext(DbContextOptions<UniversityContext> options)
           : base(options)
        {
            if (Database.EnsureCreated())
            {

                SaveChanges();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(s => s.LastName).IsRequired().HasMaxLength(50);
                entity.HasOne(s => s.Group)
                      .WithMany(g => g.Students)
                      .HasForeignKey(s => s.GroupId);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(t => t.LastName).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<SubjectGroup>(entity =>
            {
                entity.HasKey(sg => sg.Id);

                entity.HasOne(sg => sg.Subject)
                      .WithMany(s => s.SubjectsGroups)
                      .HasForeignKey(sg => sg.SubjectId);

                entity.HasOne(sg => sg.Group)
                      .WithMany(g => g.SubjectsGroups)
                      .HasForeignKey(sg => sg.GroupId);

                entity.HasOne(sg => sg.Teacher)
                      .WithMany(t => t.SubjectsGroups)
                      .HasForeignKey(sg => sg.TeacherId);
            });

            modelBuilder.Entity<Group>().HasData(
                new Group { Id = 1, Name = "A" },
                new Group { Id = 2, Name = "B" },
                new Group { Id = 3, Name = "C" }
            );

            modelBuilder.Entity<Subject>().HasData(
                new Subject { Id = 1, Name = "Data Structures" },
                new Subject { Id = 2, Name = "Quantum Physics" },
                new Subject { Id = 3, Name = "Calculus" }
            );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, FirstName = "Alice", LastName = "Johnson" },
                new Teacher { Id = 2, FirstName = "Bob", LastName = "Smith" },
                new Teacher { Id = 3, FirstName = "Charlie", LastName = "Williams" }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, FirstName = "John", LastName = "Doe", GroupId = 1 },
                new Student { Id = 2, FirstName = "Jane", LastName = "Doe", GroupId = 2 },
                new Student { Id = 3, FirstName = "Jim", LastName = "Beam", GroupId = 3 }
            );

            modelBuilder.Entity<SubjectGroup>().HasData(
                new SubjectGroup { Id = 1, SubjectId = 1, GroupId = 1, TeacherId = 1 },
                new SubjectGroup { Id = 2, SubjectId = 2, GroupId = 2, TeacherId = 2 },
                new SubjectGroup { Id = 3, SubjectId = 3, GroupId = 3, TeacherId = 3 }
            );
        }

    }
}
