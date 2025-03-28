using Microsoft.EntityFrameworkCore;
using hw05_guestbook.Models;

namespace hw05_guestbook.Repository
{
    public class GuestbookRepository : IRepository
    {
        private readonly GuestbookContext _context;

        public GuestbookRepository(GuestbookContext context)
        {
            _context = context;
        }

        public async Task Create(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task Delete(int id)
        {
            User? user = await _context.Users.FindAsync(id);
            if ( user!=null)
                _context.Users.Remove(user);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<User>> GetUserList()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }


        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
