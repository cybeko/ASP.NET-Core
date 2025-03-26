using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace hw05_guestbook.Models
{
    public class GuestbookContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        public GuestbookContext(DbContextOptions<GuestbookContext> options)
               : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Login = "Alice123", Password = "hashed_password1"},
                new User { Id = 2, Login = "Bob456", Password = "hashed_password2"},
                new User { Id = 3, Login = "Charlie789", Password = "hashed_password3"}
            );

            modelBuilder.Entity<Message>().HasData(
                new Message { Id = 1, Text = "Hello, world!", DateTime = DateTime.UtcNow, UserId = 1 },
                new Message { Id = 2, Text = "This is a test message.", DateTime = DateTime.UtcNow.AddMinutes(-30), UserId = 2 },
                new Message { Id = 3, Text = "Entity Framework Core is cool!", DateTime = DateTime.UtcNow.AddHours(-1), UserId = 3 }
            );
        }
    }
}
