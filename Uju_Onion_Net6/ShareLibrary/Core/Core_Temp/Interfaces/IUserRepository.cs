using Core_Book.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Temp.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        // Thêm các phương thức riêng nếu cần
        Task<User> GetUserByEmailAsync(string email);
        Task<User> ValidateUserAsync(string username, string password)
    }
}



