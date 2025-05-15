using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Repositories
{
    public class GenreEfRepository : IGenreRepository
    {
        private readonly DataContext _dbContext;


        public GenreEfRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }



       public async Task<List<Genre>> GetAllAsync()
       {
           return await _dbContext.Genres.ToListAsync();
       }

       public async Task<Genre> GetByIdAsync(int id)
       {
            return await _dbContext.Genres.FindAsync(id);
       }


       public async Task AddAsync(Genre genre)
       {
            await _dbContext.Genres.AddAsync(genre);
            await _dbContext.SaveChangesAsync();
       }


       public async Task UpdateAsync(Genre genre)
       {
            _dbContext.Genres.Update(genre);
            await _dbContext.SaveChangesAsync();
       }


        public async Task<bool> DeleteAsync(int id)
        {
            var genre = await _dbContext.Genres.FindAsync(id);
            if (genre == null) { return false; }

            _dbContext.Genres.Remove(genre);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}