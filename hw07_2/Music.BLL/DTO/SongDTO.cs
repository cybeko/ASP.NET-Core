using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.BLL.DTO
{
    public class SongDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Field is required.")]
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public string? Author { get; set; }
    }
}
