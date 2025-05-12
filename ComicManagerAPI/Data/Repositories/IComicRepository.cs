using Models;

namespace ComicManagerAPI.Data
{
    public interface IComicRepository
    {
        Task<List <Comic>> GetAllAsync ();
        Task<Comic> GetByIdAsync (int id);
        Task AddAsync(Comic comic);
        Task UpdateAsync(Comic comic);
        Task<bool> DeleteAsync(int id);
        Task InitDataAsync();
    }
}