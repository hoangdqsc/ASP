// Application_Book/Services/UserDtoService.cs
using Application_Book.DTOs;
using Application_Book.Helpers;
using Core_Book.Entites;
using Core_Book.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application_Temp.Services
{
    public class UserDtoService
    {
        // IUserRepository nằm trong Core_Book.Interfaces
        private readonly IUserRepository _userRepository;

        // Inject IUserRepository thông qua constructor
        public UserDtoService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Lấy tất cả người dùng và chuyển đổi sang UserDto
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            });
        }

        // Lấy người dùng theo ID và chuyển đổi sang UserDto
        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        // Thêm người dùng mới sử dụng UserDto
        public async Task AddUserAsync(UserDto userDto)
        {
            // Kiểm tra tính hợp lệ của mật khẩu tạo mới ( có 6 ký tự, viết HOA.......
            if (!PasswordHelper.ValidateNewPassword(userDto.PasswordHash))
            {
                throw new ArgumentException("Password không hợp lệ");
            }

            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                // mã hóa mật khẩu
                PasswordHash = PasswordHelper.HashPassword(userDto.PasswordHash) // mã hóa mật khẩu
            };

            await _userRepository.AddAsync(user);
            userDto.Id = user.Id; // Gán ID sau khi lưu vào cơ sở dữ liệu
        }

        // Xác thực người dùng khi đăng nhập
        public async Task<bool> AuthenticateUserAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            // So sánh mật khẩu với hash đã lưu
            return PasswordHelper.ValidatePassword(user.PasswordHash, password);
        }

        // Cập nhật người dùng bằng UserDto
        public async Task UpdateUserAsync(UserDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(userDto.Id);
            if (user != null)
            {
                user.Name = userDto.Name;
                user.Email = userDto.Email;
                await _userRepository.UpdateAsync(user);
            }
        }

        // Xóa người dùng theo ID
        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
