using Models;

namespace Data.Repositories
{
    public interface IUserComicRepository
    {
        Task AddAsync(UserComic userComic);
        Task<bool> DeleteAsync(int userId, int comicId);
        Task<bool> ExistsAsync(int userId, int comicId);
        Task<List<User>> GetUsersByComicIdAsync(int comicId);
        Task<List<Comic>> GetComicsByUserIdAsync(int userId);
        Task<UserComic> GetByIdAsync(int comicId, int genreId);
    }
}