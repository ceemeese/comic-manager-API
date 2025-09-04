using Data.Repositories;
using Models;

namespace Business
{
    
    public class UserComicService : IUserComicService
    {
        private readonly IUserComicRepository _usercomicRepository;
        private readonly IUserRepository _userRepository;
        private readonly IComicRepository _comicRepository;

        public UserComicService(IUserComicRepository usercomicRepository, IUserRepository userEfRepository, IComicRepository comicRepository)
        {
            _usercomicRepository = usercomicRepository;
            _userRepository = userEfRepository;
            _comicRepository = comicRepository;
        }


        public async Task<UserComicDtoOut> AddAsync(UserComicDtoIn usercomic)
        {

            var userComicEntity = new UserComic
            {
                UserId = usercomic.UserId,
                ComicId = usercomic.ComicId
            };

            var created = await _usercomicRepository.AddAsync(userComicEntity);

            var newRelation = new UserComicDtoOut()
            {
                UserId = created.UserId,
                ComicId = created.ComicId
            };

            return newRelation;
        }



        public async Task DeleteAsync(int userId, int comicId)
        {
            var relation = await _usercomicRepository.GetByIdAsync(userId, comicId);
            if (relation == null) 
            {
                throw new KeyNotFoundException("Relación User-Comic no encontrada");
            }

            await _usercomicRepository.DeleteAsync(relation);
        }


        public async Task ExistsAsync(int userId, int comicId)
        {
            var relation = await _usercomicRepository.GetByIdAsync(userId, comicId);
            if (relation == null) 
            {
                throw new KeyNotFoundException("Relación User-Comic no encontrada");
            }
        }


        public async Task<List<UserDtoOut>> GetUsersByComicIdAsync(int comicId)
        {
            var comicExists = await _comicRepository.GetByIdAsync(comicId);
            if (comicExists == null)
            {
                throw new KeyNotFoundException($"Cómic con id {comicId} no encontrado");
            }
            var users = await _usercomicRepository.GetUsersByComicIdAsync(comicId);

            return users.Select(u => new UserDtoOut
            {
                Id = u.Id,
                Name = u.Name,
                Mail = u.Mail,
                DateCreated = u.DateCreated,
                Telephone = u.Telephone,
                Role = u.Role
            }).ToList();
        }


        public async Task<List<ComicDtoOut>> GetComicsByUserIdAsync(int userId)
        {
            var userExists = await _userRepository.GetByIdAsync(userId);
            if (userExists == null)
            {
                throw new KeyNotFoundException($"Usuario con id {userId} no encontrado");
            }

            var comics = await _usercomicRepository.GetComicsByUserIdAsync(userId);
            return comics.Select(c => new ComicDtoOut
            {
                Id = c.Id,
                Name = c.Name,
                Author = c.Author,
                YearPublished = c.YearPublished,
                IsRead = c.IsRead,
                Type = c.Type.ToString()
            }).ToList();
        }


        public async Task<UserComic?> GetByIdAsync(int userId, int comicId)
        {
            return await _usercomicRepository.GetByIdAsync(userId, comicId);
        }
    }
    
}