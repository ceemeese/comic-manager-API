using Models;

namespace Business
{
    public interface IComicGenreService
    {
        Task<ComicGenreDtoOut> AddAsync(ComicGenreDtoIn comicGenre);
        Task DeleteAsync(int comicId, int genreId);
        Task<List<GenreDtoOut>> GetGenresByComicIdAsync(int comicId);
        Task<List<ComicDtoOut>> GetComicsByGenreIdAsync(int genreId);
        Task<ComicGenre?> GetByIdAsync(int comicId, int genreId);

    }
}