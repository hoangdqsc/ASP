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
        Task<User> GetByUsernameAsync(string username);        
              
        // Các phương thức đặc thù cho User để tác động database dùng procdure      
        Task<IEnumerable<User>> Proce_GetAll_User();
        Task<User> GetBProce_GetBtyId_User(int id);
        Task Proce_Add_User(User user);
        Task Proce_Update_User(User user);
        Task Proce_Delete_User(int id);
        
    }
}



