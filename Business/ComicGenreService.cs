using Data.Repositories;
using Models;



namespace Business
{
    
    public class ComicGenreService : IComicGenreService
    {
        private readonly IComicGenreRepository _comicgenreRepository;
        private readonly IComicRepository _comicRepository;
        private readonly IGenreRepository _genreRepository;

        public ComicGenreService(IComicGenreRepository comicgenreRepository, IComicRepository comicRepository, IGenreRepository genreRepository)
        {
            _comicgenreRepository = comicgenreRepository;
            _comicRepository = comicRepository;
            _genreRepository = genreRepository;
        }


        public async Task<ComicGenreDtoOut> AddAsync(ComicGenreDtoIn comicgenre)
        {

            if (comicgenre == null)
                throw new ArgumentNullException(nameof(comicgenre));

            _ = await _comicRepository.GetByIdAsync(comicgenre.ComicId)
                ?? throw new KeyNotFoundException($"Cómic con id {comicgenre.ComicId} no encontrado");


            _ = await _genreRepository.GetByIdAsync(comicgenre.GenreId)
                ?? throw new KeyNotFoundException($"Género con id {comicgenre.GenreId} no encontrado");


            var exists = await _comicgenreRepository.ExistsAsync(comicgenre.ComicId, comicgenre.GenreId);
            if (exists)
                throw new InvalidOperationException("La relación ya existe");


            var comicGenreEntity = new ComicGenre
            {
                ComicId = comicgenre.ComicId,
                GenreId = comicgenre.GenreId
            };

            var created = await _comicgenreRepository.AddAsync(comicGenreEntity);

            var newRelation = new ComicGenreDtoOut
            {
                ComicId = created.ComicId,
                GenreId = created.GenreId
            };
            return newRelation;
        }



        public async Task DeleteAsync(int comicId, int genreId)
        {
            var relation = await _comicgenreRepository.GetByIdAsync(comicId, genreId)
                ?? throw new KeyNotFoundException("Relación Comic-Genre no encontrada");
                
            await _comicgenreRepository.DeleteAsync(relation);
        }



        public async Task<List<GenreDtoOut>> GetGenresByComicIdAsync(int comicId)
        {
            _ = await _comicRepository.GetByIdAsync(comicId)
                ?? throw new KeyNotFoundException($"Cómic con id {comicId} no encontrado");

            var genres = await _comicgenreRepository.GetGenresByComicIdAsync(comicId);

            return genres.Select(g => new GenreDtoOut
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description,
                PercentageOfComics = g.PercentageOfComics,
                IsPopular = g.IsPopular
            }).ToList();
        }


        public async Task<List<ComicDtoOut>> GetComicsByGenreIdAsync(int genreId)
        {
            _ = await _genreRepository.GetByIdAsync(genreId)
                ?? throw new KeyNotFoundException($"Género con id {genreId} no encontrado");
           
            var comics = await _comicgenreRepository.GetComicsByGenreIdAsync(genreId);

            return comics.Select(c => new ComicDtoOut
            {
                Id = c.Id,
                Name = c.Name,
                Author = c.Author,
                YearPublished = c.YearPublished,
                Type = c.Type.ToString()
            }).ToList();
        }

        public async Task<ComicGenre?> GetByIdAsync(int comicId, int genreId)
        {
            var comicgenre = await _comicgenreRepository.GetByIdAsync(comicId, genreId);
            if (comicgenre == null)
            {
                throw new KeyNotFoundException($"Relación entre cómic con id {comicId} y género con id {genreId} no encontrada");
            };
            return comicgenre;
        }

    }
    
}