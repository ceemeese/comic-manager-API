using ComicManagerAPI.Data;
using Models;

namespace ComicManagerAPI.Models
{
    
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _GenreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _GenreRepository = genreRepository;
        }


        public async Task<List<Genre>> GetAllAsync()
        {
            return await _GenreRepository.GetAllAsync();
        }


        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _GenreRepository.GetByIdAsync(id);
        }


        public async Task AddAsync(Genre genre)
        {
            await _GenreRepository.AddAsync(genre);
        }


        public async Task UpdateAsync(Genre genre)
        {
            await _GenreRepository.UpdateAsync(genre);
        }


        public async Task DeleteAsync(int id)
        {
            var genre = await _GenreRepository.GetByIdAsync(id);
            if (genre == null) 
            {
                throw new KeyNotFoundException("Género no encontrado");
            }

            await _GenreRepository.DeleteAsync(id);
        }
    }
    
}