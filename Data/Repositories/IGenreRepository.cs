using Models;

namespace Data.Repositories
{
    public interface IGenreRepository
    {
        Task<List <Genre>> GetAllAsync ();
        Task<Genre> GetByIdAsync (int id);
        Task<Genre> AddAsync(Genre genre);
        Task UpdateAsync(Genre genre);
        Task DeleteAsync(Genre genre);
    }
}