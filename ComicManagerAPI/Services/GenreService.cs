using ComicManagerAPI.Data;
using Models;

namespace ComicManagerAPI.Models
{
    
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }


        public async Task<List<Genre>> GetAllAsync()
        {
            return await _genreRepository.GetAllAsync();
        }


        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _genreRepository.GetByIdAsync(id);
        }


        public async Task AddAsync(Genre genre)
        {
            await _genreRepository.AddAsync(genre);
        }


        public async Task UpdateAsync(Genre genre)
        {
            await _genreRepository.UpdateAsync(genre);
        }


        public async Task DeleteAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null) 
            {
                throw new KeyNotFoundException("Género no encontrado");
            }

            await _genreRepository.DeleteAsync(id);
        }
    }
    
}