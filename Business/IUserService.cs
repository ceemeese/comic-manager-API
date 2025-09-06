using Models;

namespace Business
{
    public interface IUserService 
    {
        Task<IEnumerable<UserDtoOut>> GetAllAsync();
        Task<UserDtoOut> GetByIdAsync(int id);
        Task<UserDtoOut> AddAsync(UserDtoIn user);
        Task UpdateAsync(int id, UserDtoIn userDto);
        Task DeleteAsync(int id);
        Task InitDataAsync();
    }
}