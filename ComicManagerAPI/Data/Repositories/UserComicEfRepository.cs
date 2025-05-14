using Microsoft.EntityFrameworkCore;
using Models;

namespace ComicManagerAPI.Data
{
    public class UserComicEfRepository : IUserComicRepository
    {
        private readonly DataContext _dbContext;


        public UserComicEfRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }


       public async Task AddAsync(UserComic userComic)
       {
            await _dbContext.UsersComics.AddAsync(userComic);
            await _dbContext.SaveChangesAsync();
       }


        public async Task<bool> DeleteAsync(int userId, int comicId)
        {
            var relation = await _dbContext.UsersComics.FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ComicId == comicId);
            if (relation == null) { return false; }

            _dbContext.UsersComics.Remove(relation);
            await _dbContext.SaveChangesAsync();
            return true;
        }


        public async Task<bool> ExistsAsync(int userId, int comicId)
        {
            var relation = await _dbContext.UsersComics.FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ComicId == comicId);
            if (relation == null) { return false; }

            return true;
        }


        public async Task<List<User>> GetUsersByComicIdAsync(int comicId)
       {
           return await _dbContext.UsersComics
            .Where(uc => uc.ComicId == comicId)
            .Select(uc => uc.User)
            .ToListAsync();
       }

       public async Task<List<Comic>> GetComicsByUserIdAsync(int userId)
       {
           return await _dbContext.UsersComics
            .Where(uc => uc.UserId == userId)
            .Select(uc => uc.Comic)
            .ToListAsync();
       }

    }
}