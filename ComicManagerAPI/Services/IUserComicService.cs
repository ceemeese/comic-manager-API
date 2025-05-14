using Models;

namespace ComicManagerAPI.Data
{
    public interface IUserComicService
    {
        Task AddAsync(UserComic userComic);
        Task DeleteAsync(int userId, int comicId);
        Task ExistsAsync(int userId, int comicId);
        Task<List<User>> GetUsersByComicIdAsync(int comicId);
        Task<List<Comic>> GetComicsByUserIdAsync(int userId);
        Task<UserComic?> GetByIdAsync(int userId, int comicId);
    }
}