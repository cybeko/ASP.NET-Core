using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MoviesMVC.Models
{
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public MovieContext(DbContextOptions<MovieContext> options)
           : base(options)
        {
            if (Database.EnsureCreated())
            {
                Movies?.Add(new Movie { Title = "Blade Runner 2049", Director = "Denis Villeneuve", Year = 2017, Genre = "Sci-Fi", Description = "Description", ImagePath = "/Files/1.jpg" });
                Movies?.Add(new Movie { Title = "The Lighthouse", Director = "Robert Eggers", Year = 2019, Genre = "Horror", Description = "Description", ImagePath = "/Files/2.jpg" });
                Movies?.Add(new Movie { Title = "Oldboy", Director = "Park Chan-wook", Year = 2003, Genre = "Thriller", Description = "Description", ImagePath = "/Files/3.jpg" });
                Movies?.Add(new Movie { Title = "Hereditary", Director = "Ari Aster", Year = 2018, Genre = "Horror", Description = "Description", ImagePath = "/Files/4.jpg" });
                Movies?.Add(new Movie { Title = "Midsommar", Director = "Ari Aster", Year = 2019, Genre = "Horror", Description = "Description", ImagePath = "/Files/5.jpg" });
                SaveChanges();
            }
        }
    }
}