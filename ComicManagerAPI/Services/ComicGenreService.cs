using ComicManagerAPI.Data;
using Models;

namespace ComicManagerAPI.Models
{
    
    public class ComicGenreService : IComicGenreService
    {
        private readonly IComicGenreRepository _comicgenreRepository;

        public ComicGenreService(IComicGenreRepository comicgenreRepository)
        {
            _comicgenreRepository = comicgenreRepository;
        }


        public async Task AddAsync(ComicGenre comicgenre)
        {
            await _comicgenreRepository.AddAsync(comicgenre);
        }



        public async Task DeleteAsync(int comicId, int genreId)
        {
            var relation = await _comicgenreRepository.GetByIdAsync(comicId, genreId);
            if (relation == null) 
            {
                throw new KeyNotFoundException("Relación Comic-Genre no encontrada");
            }

            await _comicgenreRepository.DeleteAsync(comicId, genreId);
        }


        public async Task ExistsAsync(int comicId, int genreId)
        {
             var relation = await _comicgenreRepository.GetByIdAsync(comicId, genreId);
            if (relation == null) 
            {
                throw new KeyNotFoundException("Relación Comic-Genre no encontrada");
            }
        }


        public async Task<List<Genre>> GetGenresByComicIdAsync(int comicId)
        {
            return await _comicgenreRepository.GetGenresByComicIdAsync(comicId);
        }


        public async Task<List<Comic>> GetComicsByGenreIdAsync(int genreId)
        {
            return await _comicgenreRepository.GetComicsByGenreIdAsync(genreId);
        }

        public async Task<ComicGenre?> GetByIdAsync(int comicId, int genreId)
        {
            return await _comicgenreRepository.GetByIdAsync(comicId, genreId);
        }

    }
    
}