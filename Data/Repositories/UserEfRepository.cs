using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Repositories
{
    public class UserEfRepository : IUserRepository
    {
        private readonly DataContext _dbContext;


        public UserEfRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<IEnumerable<User>> GetAllAsync()
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


        public async Task InitDataAsync()
        {
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
        

         public UserDtoOut AddUserFromCredentials(UserDtoIn userDtoIn) {
            var userId = 1;
            var user = new UserDtoOut { Id = userId, Name = userDtoIn.Name, Mail = userDtoIn.Mail, Role = Rols.User};
            if (user == null)
            {
                throw new KeyNotFoundException("Usuario no creado");
            }
            return user;
        }
        
        public UserDtoOut GetUserFromCredentials(LoginDtoIn loginDtoIn) {
            if ((loginDtoIn.Mail != "a27877@svalero.com") && (loginDtoIn.Password != "1234"))
            {
                throw new KeyNotFoundException("Usuario no encontrado");
            } else {
                var user = new UserDtoOut { Id = 1, Name = "cmalmierca", Mail = "a27877@svalero.com", Role = Rols.Admin};
                return user;
            }
        }

    }
}