using Models;

namespace Business
{
    public interface IComicService 
    {
        Task<List<ComicDtoOut>> GetAllAsync();
        Task<ComicDtoOut> GetByIdAsync(int id);
        Task<ComicDtoOut> AddAsync(ComicDtoIn comic);
        Task UpdateAsync(int id, ComicDtoIn comic);
        Task DeleteAsync(int id);
    }
}