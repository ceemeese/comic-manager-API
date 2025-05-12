using ComicManagerAPI.Data;
using Models;

namespace ComicManagerAPI.Models
{
    
    public class ComicService : IComicService
    {
        private readonly IComicRepository _ComicRepository;

        public ComicService(IComicRepository comicRepository)
        {
            _ComicRepository = comicRepository;
        }


        public async Task<List<Comic>> GetAllAsync()
        {
            return await _ComicRepository.GetAllAsync();
        }


        public async Task<Comic> GetByIdAsync(int id)
        {
            return await _ComicRepository.GetByIdAsync(id);
        }


        public async Task AddAsync(Comic comic)
        {
            await _ComicRepository.AddAsync(comic);
        }


        public async Task UpdateAsync(Comic comic)
        {
            await _ComicRepository.UpdateAsync(comic);
        }


        public async Task DeleteAsync(int id)
        {
            var comic = await _ComicRepository.GetByIdAsync(id);
            if (comic == null) 
            {
                throw new KeyNotFoundException("Comic no encontrado");
            }

            await _ComicRepository.DeleteAsync(id);
        }
    }
    
}