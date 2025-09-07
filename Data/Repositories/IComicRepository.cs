using Models;

namespace Data.Repositories
{
    public interface IComicRepository
    {
        Task<List <Comic>> GetAllAsync ();
        Task<Comic> GetByIdAsync (int id);
        Task<Comic> AddAsync(Comic comic);
        Task UpdateAsync(Comic comic);
        Task DeleteAsync(Comic comic);
    }
}