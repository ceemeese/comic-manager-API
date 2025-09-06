using Models;

namespace Business
{
    public interface IGenreService 
    {
        Task<List<GenreDtoOut>> GetAllAsync();
        Task<GenreDtoOut> GetByIdAsync(int id);
        Task<GenreDtoOut> AddAsync(GenreDtoIn genreDto);
        Task UpdateAsync(int id, GenreDtoIn genreDto);
        Task DeleteAsync(int id);
    }
}