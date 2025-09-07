using Data.Repositories;
using Models;

namespace Business
{
    
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }


        public async Task<List<GenreDtoOut>> GetAllAsync()
        {
            var genres = await _genreRepository.GetAllAsync();

            return genres.Select(g => new GenreDtoOut
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description,
                PercentageOfComics = g.PercentageOfComics,
                IsPopular = g.IsPopular
            }).ToList();
        }


        public async Task<GenreDtoOut> GetByIdAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Género no encontrado");

            return new GenreDtoOut
            {
                Id = genre.Id,
                Name = genre.Name,
                Description = genre.Description,
                PercentageOfComics = genre.PercentageOfComics,
                IsPopular = genre.IsPopular
            };
        }


        public async Task<GenreDtoOut> AddAsync(GenreDtoIn genreDto)
        {
            //TODO: Validación si ya existe

            var genreEntity = new Genre
            {
                Name = genreDto.Name,
                Description = genreDto.Description,
                Priority = genreDto.Priority,
                Icon = genreDto.Icon
            };
            await _genreRepository.AddAsync(genreEntity);
            return new GenreDtoOut
            {
                Id = genreEntity.Id,
                Name = genreEntity.Name,
                Description = genreEntity.Description,
                Priority = genreEntity.Priority,
                Icon = genreEntity.Icon,
                PercentageOfComics = genreEntity.PercentageOfComics,
                IsPopular = genreEntity.IsPopular
            };
        }


        public async Task UpdateAsync(int id, GenreDtoIn genreDto)
        {
            var existGenre = await _genreRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Género no encontrado");

            existGenre.Name = genreDto.Name;
            existGenre.Description = genreDto.Description;
            existGenre.Priority = genreDto.Priority;
            existGenre.Icon = genreDto.Icon;

            await _genreRepository.UpdateAsync(existGenre);
        }


        public async Task DeleteAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Género no encontrado");

            await _genreRepository.DeleteAsync(genre);
        }
    }
    
}