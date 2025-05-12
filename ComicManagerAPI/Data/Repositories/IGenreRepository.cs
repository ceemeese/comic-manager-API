using Models;

namespace ComicManagerAPI.Data
{
    public interface IGenreRepository
    {
        Task<List <Genre>> GetAllAsync ();
        Task<Genre> GetByIdAsync (int id);
        Task AddAsync(Genre genre);
        Task UpdateAsync(Genre genre);
        Task<bool> DeleteAsync(int id);
    }
}