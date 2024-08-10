using Core_Temp.Entities;
using Core_Temp.Interfaces;
using Infrastructure_Temp.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Temp.Repositories
{
    public class User_procedure_Repository : IUserRepository
    {

        private readonly ApplicationDbContext _context;
        public User_procedure_Repository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user)
        {
            var parameters = new[]
            {
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@PasswordHash", user.PasswordHash),
                new SqlParameter("@Roles", user.Roles)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC AddUser @Username, @Email, @PasswordHash, @Roles", parameters);
        }     
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _context.Users
            .FromSqlRaw("EXEC GetAllUsers")
            .AsNoTracking()
            .ToListAsync();
            return users;
        }
        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _context.Users
            .FromSqlRaw("EXEC GetUserById @Id = {0}", id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
            return user;
        }
        public Task<User> GetByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }
        public async Task UpdateAsync(User user)
        {
            var parameters = new[]
            {
            new SqlParameter("@Id", user.Id),
            new SqlParameter("@Username", user.Username),
            new SqlParameter("@Email", user.Email),
            new SqlParameter("@PasswordHash", user.PasswordHash),
            new SqlParameter("@Roles", user.Roles)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC UpdateUser @Id, @Username, @Email, @PasswordHash, @Roles", parameters);
        }
        public async Task DeleteAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteUser @Id = {0}", id);
        }

    }
}
