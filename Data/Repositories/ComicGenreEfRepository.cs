using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Repositories
{
    public class ComicGenreEfRepository : IComicGenreRepository
    {
        private readonly DataContext _dbContext;


        public ComicGenreEfRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }


       public async Task AddAsync(ComicGenre comicGenre)
       {
            await _dbContext.ComicsGenres.AddAsync(comicGenre);
            await _dbContext.SaveChangesAsync();
       }


        public async Task<bool> DeleteAsync(int comicId, int genreId)
        {
            var relation = await _dbContext.ComicsGenres.FirstOrDefaultAsync(cg => cg.ComicId == comicId && cg.GenreId == genreId);
            if (relation == null) { return false; }

            _dbContext.ComicsGenres.Remove(relation);
            await _dbContext.SaveChangesAsync();
            return true;
        }


        public async Task<bool> ExistsAsync(int comicId, int genreId)
        {
            var relation = await _dbContext.ComicsGenres.FirstOrDefaultAsync(cg => cg.ComicId == comicId && cg.GenreId == genreId);
            if (relation == null) { return false; }

            return true;
        }


        public async Task<List<Genre>> GetGenresByComicIdAsync(int comicId)
       {
           return await _dbContext.ComicsGenres
            .Where(cg => cg.ComicId == comicId)
            .Select(cg => cg.Genre)
            .ToListAsync();
       }

       public async Task<List<Comic>> GetComicsByGenreIdAsync(int genreId)
       {
           return await _dbContext.ComicsGenres
            .Where(cg => cg.GenreId == genreId)
            .Select(cg => cg.Comic)
            .ToListAsync();
       }


       public async Task<ComicGenre?> GetByIdAsync(int comicId, int genreId)
        {
            return await _dbContext.ComicsGenres
                .FirstOrDefaultAsync(cg => cg.ComicId == comicId && cg.GenreId == genreId);
        }

    }
}