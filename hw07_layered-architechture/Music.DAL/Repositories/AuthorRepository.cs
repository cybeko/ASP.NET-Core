using Microsoft.EntityFrameworkCore;
using Music.DAL.EF;
using Music.DAL.Entities;
using Music.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.DAL.Repositories
{
    internal class AuthorRepository : IRepository<Author>
    {
        private readonly MusicContext db;

        public AuthorRepository(MusicContext db)
        {
            this.db = db;
        }

        public async Task Create(Author item)
        {
            await db.Author.AddAsync(item);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Author? author = await db.Author.FindAsync(id);
            if (author != null)
            {
                db.Author.Remove(author);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Author?> Get(int id)
        {
            return await db.Author.Include(a => a.Songs).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Author?> Get(string name)
        {
            return await db.Author.Include(a => a.Songs).FirstOrDefaultAsync(a => a.FirstName == name || a.LastName == name);
        }
        public async Task<IEnumerable<Author>> GetAll()
        {
            return await db.Author.Include(a => a.Songs).ToListAsync();
        }

        public void Update(Author item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
