using Models;

namespace Business
{
    public interface IUserService 
    {
        Task<IEnumerable<UserDtoOut>> GetAllAsync();
        Task<UserDtoOut> GetByIdAsync(int id);
        Task AddAsync(UserDtoIn user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task InitDataAsync();
    }
}