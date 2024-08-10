using Core_Temp.Entities;
using Core_Temp.Interfaces;
using Infrastructure_Temp.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Temp.Repositories
{

    // cho biết rằng UserRepository vừa kế thừa Repository<User> và vừa triển khai IUserRepository.
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            
        }
               
       // triển khai các phuong thức mở rộng của IUserRepository

        // Triển khai phương thức GetByUsernameAsync từ IUserRepository
        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        // Triển khai các phương thức đặc thù cho User, tác động với Database bằng thủ tục Procedure
        public async Task<IEnumerable<User>> Proce_GetAll_User()
        {
            // Triển khai logic để lấy tất cả User
            var users = await _context.Users
            .FromSqlRaw("EXEC GetAllUsers")
            .AsNoTracking()
            .ToListAsync();
            return users;
        }      
      public async Task<User> GetBProce_GetBtyId_User(int id)
      {
            // Triển khai logic để lấy User theo ID
            var user = await _context.Users
             .FromSqlRaw("EXEC GetUserById @Id = {0}", id)
             .AsNoTracking()
             .FirstOrDefaultAsync();
            return user;
        }
      public async Task Proce_Add_User(User user)
      {
            // Triển khai logic để thêm mới User
            var parameters = new[]
                  {
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@PasswordHash", user.PasswordHash),
                new SqlParameter("@Roles", user.Roles)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC AddUser @Username, @Email, @PasswordHash, @Roles", parameters);
        }
        public async Task Proce_Update_User(User user)
        {
            // Triển khai logic để cập nhật User
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
        public async Task Proce_Delete_User(int id)
        {
            // Triển khai logic để xóa User theo ID
            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteUser @Id = {0}", id);
        }       
    }
}
