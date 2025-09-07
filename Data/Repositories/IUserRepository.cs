using Models;

namespace Data.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task InitDataAsync();
        Task<User> AddUserFromCredentials(UserDtoIn userDtoIn, string hashedPassword);
        Task<User> GetUserByMail(string email);
    }
}