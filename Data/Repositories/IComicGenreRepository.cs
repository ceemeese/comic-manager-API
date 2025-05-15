using Models;

namespace Data.Repositories
{
    public interface IComicGenreRepository
    {
        Task AddAsync(ComicGenre comicGenre);
        Task<bool> DeleteAsync(int comicId, int genreId);
        Task<bool> ExistsAsync(int comicId, int genreId);
        Task<List<Genre>> GetGenresByComicIdAsync(int comicId);
        Task<List<Comic>> GetComicsByGenreIdAsync(int genreId);
        Task<ComicGenre> GetByIdAsync(int comicId, int genreId);
    }
}