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
        public UserDtoOut AddUserFromCredentials(UserDtoIn userDtoIn);
        public UserDtoOut GetUserFromCredentials(LoginDtoIn loginDtoIn);
    }
}