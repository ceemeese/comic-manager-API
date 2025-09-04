using Models;

namespace Data.Repositories
{
    public interface IUserComicRepository
    {
        Task<UserComic> AddAsync(UserComic userComic);
        Task<bool> DeleteAsync(UserComic relation);
        Task<bool> ExistsAsync(int userId, int comicId);
        Task<List<User>> GetUsersByComicIdAsync(int comicId);
        Task<List<Comic>> GetComicsByUserIdAsync(int userId);
        Task<UserComic?> GetByIdAsync(int userId, int comicId);
    }
}