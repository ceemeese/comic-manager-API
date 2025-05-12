using ComicManagerAPI.Data;
using Models;

namespace ComicManagerAPI.Models
{
    
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;

        public UserService(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }


        public async Task<List<User>> GetAllAsync()
        {
            return await _UserRepository.GetAllAsync();
        }


        public async Task<User> GetByIdAsync(int id)
        {
            return await _UserRepository.GetByIdAsync(id);
        }


        public async Task AddAsync(User user)
        {
            await _UserRepository.AddAsync(user);
        }


        public async Task UpdateAsync(User user)
        {
            await _UserRepository.UpdateAsync(user);
        }


        public async Task DeleteAsync(int id)
        {
            var user = await _UserRepository.GetByIdAsync(id);
            if (user == null) 
            {
                throw new KeyNotFoundException("Usuario no encontrado");
            }

            await _UserRepository.DeleteAsync(id);
        }


        public async Task InitDataAsync()
        {
            await _UserRepository.InitDataAsync();
        }
    }
    
}