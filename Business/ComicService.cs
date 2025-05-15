using Data.Repositories;
using Models;

namespace Business
{
    
    public class ComicService : IComicService
    {
        private readonly IComicRepository _comicRepository;

        public ComicService(IComicRepository comicRepository)
        {
            _comicRepository = comicRepository;
        }


        public async Task<List<Comic>> GetAllAsync()
        {
            return await _comicRepository.GetAllAsync();
        }


        public async Task<Comic> GetByIdAsync(int id)
        {
            return await _comicRepository.GetByIdAsync(id);
        }


        public async Task AddAsync(Comic comic)
        {
            await _comicRepository.AddAsync(comic);
        }


        public async Task UpdateAsync(Comic comic)
        {
            await _comicRepository.UpdateAsync(comic);
        }


        public async Task DeleteAsync(int id)
        {
            var comic = await _comicRepository.GetByIdAsync(id);
            if (comic == null) 
            {
                throw new KeyNotFoundException("Comic no encontrado");
            }

            await _comicRepository.DeleteAsync(id);
        }
    }
    
}