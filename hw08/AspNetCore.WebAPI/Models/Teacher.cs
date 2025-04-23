using Microsoft.EntityFrameworkCore;

namespace AspNetCore.WebAPI.Models
{
    public class TeacherContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }

        public TeacherContext(DbContextOptions<TeacherContext> options)
            : base(options)
        {
            if (Database.EnsureCreated())
            {
                Teachers.Add(new Teacher { Name = "Иван", Surname = "Иваненко", Age = 30 });
                Teachers.Add(new Teacher { Name = "Сергей", Surname = "Алексеенко", Age = 40 });
                Teachers.Add(new Teacher { Name = "Петр", Surname = "Петренко", Age = 50 });
                SaveChanges();
            }
        }
    }

    public class Teacher
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int Age { get; set; }
    }
}
