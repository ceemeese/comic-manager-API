using ComicManagerAPI.Data;
using Models;

namespace ComicManagerAPI.Models
{
    
    public class UserComicService : IUserComicService
    {
        private readonly IUserComicRepository _usercomicRepository;

        public UserComicService(IUserComicRepository usercomicRepository)
        {
            _usercomicRepository = usercomicRepository;
        }


        public async Task AddAsync(UserComic usercomic)
        {
            await _usercomicRepository.AddAsync(usercomic);
        }



        public async Task DeleteAsync(int userId, int comicId)
        {
            var relation = await _usercomicRepository.GetByIdAsync(userId, comicId);
            if (relation == null) 
            {
                throw new KeyNotFoundException("Relación User-Comic no encontrada");
            }

            await _usercomicRepository.DeleteAsync(userId, comicId);
        }


        public async Task ExistsAsync(int userId, int comicId)
        {
            var relation = await _usercomicRepository.GetByIdAsync(userId, comicId);
            if (relation == null) 
            {
                throw new KeyNotFoundException("Relación User-Comic no encontrada");
            }
        }


        public async Task<List<User>> GetUsersByComicIdAsync(int comicId)
        {
            return await _usercomicRepository.GetUsersByComicIdAsync(comicId);
        }


        public async Task<List<Comic>> GetComicsByUserIdAsync(int userId)
        {
            return await _usercomicRepository.GetComicsByUserIdAsync(userId);
        }


        public async Task<UserComic?> GetByIdAsync(int userId, int comicId)
        {
            return await _usercomicRepository.GetByIdAsync(userId, comicId);
        }
    }
    
}