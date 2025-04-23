using Microsoft.AspNetCore.Mvc.Rendering;

namespace Soccer.Models
{
    public class UserListViewModel
    {
        public IEnumerable<Players> Players { get; set; } = new List<Players>();
        public SelectList Teams { get; set; } = new SelectList(new List<Teams>(), "Id", "Name");
        public string? Position { get; set; }
    }
    public class TeamListViewModel
    {
        public IEnumerable<Teams> TeamsList { get; set; } = new List<Teams>();
        public SelectList? Coaches { get; set; }
        public string? Coach { get; set; }
    }
}
