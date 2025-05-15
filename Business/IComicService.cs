using Models;

namespace Business
{
    public interface IComicService 
    {
        Task<List<Comic>> GetAllAsync();
        Task<Comic> GetByIdAsync(int id);
        Task AddAsync(Comic comic);
        Task UpdateAsync(Comic comic);
        Task DeleteAsync(int id);
    }
}