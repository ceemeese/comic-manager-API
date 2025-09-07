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


        public async Task<Genre> AddAsync(Genre genre)
        {
            var created = await _dbContext.Genres.AddAsync(genre);
            await _dbContext.SaveChangesAsync();
            return created.Entity;
       }


       public async Task UpdateAsync(Genre genre)
       {
            _dbContext.Genres.Update(genre);
            await _dbContext.SaveChangesAsync();
       }


        public async Task DeleteAsync(Genre genre)
        {
            _dbContext.Genres.Remove(genre);
            await _dbContext.SaveChangesAsync();
        }

    }
}