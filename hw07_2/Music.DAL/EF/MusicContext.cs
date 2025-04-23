using Microsoft.EntityFrameworkCore;
using Music.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.DAL.EF
{
    public class MusicContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
        public MusicContext(DbContextOptions<MusicContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
