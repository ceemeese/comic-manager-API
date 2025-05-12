using Microsoft.EntityFrameworkCore;
using Models;

namespace ComicManagerAPI.Data
{
    public class UserEfRepository : IUserRepository
    {
        private readonly DataContext _dbContext;


        public UserEfRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }



       public async Task<List<User>> GetAllAsync()
       {
           return await _dbContext.Users.ToListAsync();
       }

       public async Task<User> GetByIdAsync(int id)
       {
            return await _dbContext.Users.FindAsync(id);
       }


       public async Task AddAsync(User user)
       {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
       }


       public async Task UpdateAsync(User user)
       {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
       }


        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null) { return false; }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }


        public async Task InitDataAsync() {
            if (!await _dbContext.Users.AnyAsync())
           {
               var users = new List<User>
               {
                   new User { Name = "Admin", Mail = "admin@admin.com", Password = "admin", Telephone = "666666666" },
                   new User { Name = "Test", Mail = "test@test.com", Password = "test", Telephone = "666666661" }
               };

               await _dbContext.Users.AddRangeAsync(users);
               await _dbContext.SaveChangesAsync();
           }
        }

    }
}