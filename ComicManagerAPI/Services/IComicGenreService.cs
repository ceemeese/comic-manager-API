using Models;

namespace ComicManagerAPI.Data
{
    public interface IComicGenreService
    {
        Task AddAsync(ComicGenre comicGenre);
        Task DeleteAsync(int comicId, int genreId);
        Task ExistsAsync(int comicId, int genreId);
        Task<List<Genre>> GetGenresByComicIdAsync(int comicId);
        Task<List<Comic>> GetComicsByGenreIdAsync(int genreId);
        Task<ComicGenre?> GetByIdAsync(int comicId, int genreId);

    }
}