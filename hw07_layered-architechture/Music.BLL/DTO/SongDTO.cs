using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.BLL.DTO
{
    internal class SongDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Field is required.")]
        public string? Title { get; set; }
        public int? GenreId { get; set; }
        public string? Genre { get; set; }
        public int? AuthorId { get; set; }
        public string? Author { get; set; }
    }
}
