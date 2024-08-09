using Core_Temp.Entities;
using Core_Temp.Interfaces;
using Infrastructure_Temp.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Temp.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }
    }
}
