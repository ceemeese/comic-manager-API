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


        public async Task<IEnumerable<UserDtoOut>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDtoOut
            {
                Id = u.Id,
                Name = u.Name,
                Mail = u.Mail,
                DateCreated = u.DateCreated,
                Telephone = u.Telephone
            });
        }


        public async Task<UserDtoOut> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return new UserDtoOut
            {
                Id = user.Id,
                Name = user.Name,
                Mail = user.Mail,
                DateCreated = user.DateCreated,
                Telephone = user.Telephone
            };
        }


        public async Task AddAsync(UserDtoIn userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                Mail = userDto.Mail,
                Password = userDto.Password,
                Telephone = userDto.Telephone
            };

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