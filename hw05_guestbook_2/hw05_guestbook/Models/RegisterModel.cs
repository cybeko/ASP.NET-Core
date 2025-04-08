using System.ComponentModel.DataAnnotations;

namespace hw05_guestbook.Models
{
    public class RegisterModel
    {
        [Required]
        public string? Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
