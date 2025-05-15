using Models;
using Data.Repositories;

namespace Business
{
    
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<List<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }


        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }


        public async Task AddAsync(User user)
        {
            await _userRepository.AddAsync(user);
        }


        public async Task UpdateAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
        }


        public async Task DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) 
            {
                throw new KeyNotFoundException("Usuario no encontrado");
            }

            await _userRepository.DeleteAsync(id);
        }


        public async Task InitDataAsync()
        {
            await _userRepository.InitDataAsync();
        }
    }
    
}