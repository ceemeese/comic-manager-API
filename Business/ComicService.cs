using Data.Repositories;
using Models;
using Microsoft.EntityFrameworkCore;

namespace Business
{

    public class ComicService : IComicService
    {
        private readonly IComicRepository _comicRepository;

        public ComicService(IComicRepository comicRepository)
        {
            _comicRepository = comicRepository;
        }


        public async Task<List<ComicDtoOut>> GetAllAsync()
        {
            var comics = await _comicRepository.GetAllAsync();

            return comics.Select(c => new ComicDtoOut
            {
                Id = c.Id,
                Name = c.Name,
                Author = c.Author,
                Publisher = c.Publisher,
                YearPublished = c.YearPublished,
                Price = c.Price,
                IsForAdults = c.IsForAdults,
                DateCreated = c.DateCreated,
                Type = c.Type.ToString()
            }).ToList();
        }


        public async Task<ComicDtoOut> GetByIdAsync(int id)
        {
            var comic = await _comicRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Cómic no encontrado");

            return new ComicDtoOut
            {
                Id = comic.Id,
                Name = comic.Name,
                Author = comic.Author,
                Publisher = comic.Publisher,
                YearPublished = comic.YearPublished,
                Price = comic.Price,
                IsForAdults = comic.IsForAdults,
                DateCreated = comic.DateCreated,
                Type = comic.Type.ToString()
            };
        }


        public async Task<ComicDtoOut> AddAsync(ComicDtoIn comicDto)
        {
            //TODO: Validación si ya existe

            var comicEntity = new Comic
            {
                Name = comicDto.Name,
                Author = comicDto.Author,
                Publisher = comicDto.Publisher,
                YearPublished = comicDto.YearPublished,
                Price = comicDto.Price,
                IsForAdults = comicDto.IsForAdults,
                Type = comicDto.Type,

            };

            var createdComic = await _comicRepository.AddAsync(comicEntity);
            return new ComicDtoOut
            {
                Id = createdComic.Id,
                Name = createdComic.Name,
                Author = createdComic.Author,
                Publisher = createdComic.Publisher,
                Price = createdComic.Price,
                YearPublished = createdComic.YearPublished,
                IsForAdults = createdComic.IsForAdults,
                DateCreated = createdComic.DateCreated,
                Type = createdComic.Type.ToString()
            };
        }


        public async Task UpdateAsync(int id, ComicDtoIn comicDto)
        {
            var existComic = await _comicRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Comic no encontrado");

            existComic.Name = comicDto.Name;
            existComic.Author = comicDto.Author;
            existComic.Publisher = comicDto.Publisher;
            existComic.YearPublished = comicDto.YearPublished;
            existComic.Price = comicDto.Price;
            existComic.IsForAdults = comicDto.IsForAdults;
            existComic.Type = comicDto.Type;

            await _comicRepository.UpdateAsync(existComic);
        }


        public async Task DeleteAsync(int id)
        {
            var comic = await _comicRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Comic no encontrado");

            await _comicRepository.DeleteAsync(comic);
        }
        

        public async Task<List<ComicDtoOut>> SearchComics(ComicQueryParameters queryParameters)
        {
            IQueryable<Comic> query = _comicRepository.SearchComics();

            
            if (!string.IsNullOrEmpty(queryParameters.Name))
            {
                query = query.Where(c => c.Name.Contains(queryParameters.Name));
            }

            if (!string.IsNullOrEmpty(queryParameters.Author))
            {
                query = query.Where(c => c.Author.Contains(queryParameters.Author));
            }

            if (!string.IsNullOrEmpty(queryParameters.Type))
            {
                if (Enum.TryParse<Comic.ComicType>(queryParameters.Type, true, out var comicType))
                {
                    query = query.Where(c => c.Type == comicType);
                }
                else
                {
                    throw new ArgumentException("Tipo de cómic no válido");
                }
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.SortBy))
            {
                switch (queryParameters.SortBy.ToLower())
                {
                    case "price":
                        query = queryParameters.SortDescending
                            ? query.OrderByDescending(c => c.Price)
                            : query.OrderBy(c => c.Price);
                        break;
                    case "yearpublished":
                        query = queryParameters.SortDescending
                            ? query.OrderByDescending(c => c.YearPublished)
                            : query.OrderBy(c => c.YearPublished);
                        break;
                    default:
                        throw new ArgumentException("Campo para ordenar no válido");
                }
            }

            var comics = await query.ToListAsync();

            return comics.Select(c => new ComicDtoOut
            {
                Id = c.Id,
                Name = c.Name,
                Author = c.Author,
                Publisher = c.Publisher,
                YearPublished = c.YearPublished,
                Price = c.Price,
                IsForAdults = c.IsForAdults,
                DateCreated = c.DateCreated,
                Type = c.Type.ToString()
            }).ToList();
        }
    }
    
}