using hw05_guestbook.Models;

namespace hw05_guestbook.Repository
{
    public interface IRepository
    {
        Task<List<User>> GetUserList();
        Task<User> GetUserById(int id);
        Task Create(User user);
        void Update(User user); 
        Task Delete(int id);    
        Task Save();
    }
}
