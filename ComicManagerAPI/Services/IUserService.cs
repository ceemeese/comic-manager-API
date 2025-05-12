using Models;

namespace ComicManagerAPI.Data
{
    public interface IUserService 
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task InitDataAsync();
    }
}