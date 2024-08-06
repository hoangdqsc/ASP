// Application_Book/Services/UserService.cs
using Core_Book.Entites;
using Core_Book.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application_Temp.Services
{
    public class UserService
    {
        // IUserRepository nằm trong Core_Book.Interfaces
        private readonly IUserRepository _userRepository;

        // Inject IUserRepository thông qua constructor
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Lấy tất cả người dùng
        public Task<IEnumerable<User>> GetAllUsersAsync() => _userRepository.GetAllAsync();

        // Lấy người dùng theo ID
        public Task<User> GetUserByIdAsync(int id) => _userRepository.GetByIdAsync(id);

        // Thêm người dùng mới
        public Task AddUserAsync(User user) => _userRepository.AddAsync(user);

        // Cập nhật người dùng
        public Task UpdateUserAsync(User user) => _userRepository.UpdateAsync(user);

        // Xóa người dùng theo ID
        public Task DeleteUserAsync(int id) => _userRepository.DeleteAsync(id);

        // Lấy người dùng theo email
        public Task<User> GetUserByEmailAsync(string email) => _userRepository.GetUserByEmailAsync(email);
    }
}
