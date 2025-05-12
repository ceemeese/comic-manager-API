using Microsoft.EntityFrameworkCore;
using Models;

namespace ComicManagerAPI.Data
{
    public class ComicEfRepository : IComicRepository
    {
        private readonly DataContext _dbContext;


        public ComicEfRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }



       public async Task<List<Comic>> GetAllAsync()
       {
           return await _dbContext.Comics.ToListAsync();
       }

       public async Task<Comic> GetByIdAsync(int id)
       {
            return await _dbContext.Comics.FindAsync(id);
       }


       public async Task AddAsync(Comic comic)
       {
            await _dbContext.Comics.AddAsync(comic);
            await _dbContext.SaveChangesAsync();
       }


       public async Task UpdateAsync(Comic comic)
       {
            _dbContext.Comics.Update(comic);
            await _dbContext.SaveChangesAsync();
       }


        public async Task<bool> DeleteAsync(int id)
        {
            var comic = await _dbContext.Comics.FindAsync(id);
            if (comic == null) { return false; }

            _dbContext.Comics.Remove(comic);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}