using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Repositories
{
    public class UserComicEfRepository : IUserComicRepository
    {
        private readonly DataContext _dbContext;


        public UserComicEfRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<UserComic> AddAsync(UserComic userComic)
        {
            var created = await _dbContext.UsersComics.AddAsync(userComic);
            await _dbContext.SaveChangesAsync();
            return created.Entity;
        }


        public async Task<bool> DeleteAsync(UserComic relation)
        {
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


        public async Task<UserComic?> GetByIdAsync(int userId, int comicId)
        {
            return await _dbContext.UsersComics
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ComicId == comicId);
        }
        

        public async Task UpdateAsync(UserComic usercomic)
       {
            _dbContext.UsersComics.Update(usercomic);
            await _dbContext.SaveChangesAsync();
       }

    }
}