using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoviesMVC.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string? Title { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string? Description { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string? Genre { get; set; }

        [Required]
        [Range(1000, 9999, ErrorMessage = "Year must be a four-digit number.")]
        public int Year { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string? Director { get; set; }

        public string? ImagePath { get; set; }
    }
}