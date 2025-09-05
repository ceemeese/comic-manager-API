using Models;

namespace Business
{
    public interface IUserComicService
    {
        Task <UserComicDtoOut> AddAsync(UserComicDtoIn userComic);
        Task DeleteAsync(int userId, int comicId);
        Task ExistsAsync(int userId, int comicId);
        Task<List<UserDtoOut>> GetUsersByComicIdAsync(int comicId);
        Task<List<ComicDtoOut>> GetComicsByUserIdAsync(int userId);
        Task<UserComic?> GetByIdAsync(int userId, int comicId);
    }
}