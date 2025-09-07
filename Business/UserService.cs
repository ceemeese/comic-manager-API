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


        public async Task<UserDtoOut> AddAsync(UserDtoIn userDto)
        {
            //TODO: Validación si ya existe
            
            var userEntity = new User
            {
                Name = userDto.Name,
                Mail = userDto.Mail,
                Password = userDto.Password,
                Telephone = userDto.Telephone
            };

            var createdUser = await _userRepository.AddAsync(userEntity);

            return new UserDtoOut
            {
                Id = createdUser.Id,
                Name = createdUser.Name,
                Mail = createdUser.Mail,
                DateCreated = createdUser.DateCreated,
                Telephone = createdUser.Telephone
            };
        }


        public async Task UpdateAsync(int id, UserDtoIn userDto)
        {
            var existingUser = await _userRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Usuario no encontrado");

            existingUser.Name = userDto.Name;
            existingUser.Mail = userDto.Mail;
            existingUser.Password = userDto.Password;
            existingUser.Telephone = userDto.Telephone;

            await _userRepository.UpdateAsync(existingUser);
        }


        public async Task DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Usuario no encontrado");

            await _userRepository.DeleteAsync(user);
        }


        public async Task InitDataAsync()
        {
            await _userRepository.InitDataAsync();
        }
    }
    
}