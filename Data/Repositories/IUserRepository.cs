using Models;

namespace Data.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
        Task InitDataAsync();
        Task<User> AddUserFromCredentials(UserDtoIn userDtoIn, string hashedPassword);
        Task<User> GetUserByMail(string email);
    }
}