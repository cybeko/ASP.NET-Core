using System.ComponentModel.DataAnnotations;

namespace hw05_guestbook.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string? Salt { get; set; }
        public Message? Message { get; set; } 
    }

    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }

}
