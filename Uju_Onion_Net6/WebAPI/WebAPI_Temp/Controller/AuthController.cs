using Application_Temp.DTOs; // Import namespace chứa các Data Transfer Objects (DTOs).
using Application_Temp.Services; // Import namespace chứa các dịch vụ nghiệp vụ.
using Microsoft.AspNetCore.Mvc; // Import namespace cho ASP.NET Core MVC, cung cấp các lớp cơ bản cho controller.
using System.Threading.Tasks; // Import namespace hỗ trợ lập trình bất đồng bộ với Task, async, await.

namespace API_Temp.Controllers // Định nghĩa không gian tên cho controller AuthController.
{
    [ApiController] // Đánh dấu lớp này là một API Controller, cung cấp các tính năng tự động cho API như kiểm tra model, định dạng phản hồi JSON.
    [Route("api/[controller]")] // Định nghĩa tuyến URL cho controller, trong đó [controller] sẽ được thay bằng tên controller (Auth).
    public class AuthController : ControllerBase // Kế thừa từ ControllerBase, cung cấp các tính năng cơ bản cho API controller.
    {
        private readonly IUserService _userService; // Khai báo biến private chỉ đọc để giữ một instance của IUserService.

        public AuthController(IUserService userService) // Constructor nhận vào IUserService, dịch vụ xử lý logic người dùng.
        {
            _userService = userService; // Gán giá trị userService nhận vào cho biến _userService để sử dụng trong các phương thức của controller.
        }

        [HttpPost("register")] // Đánh dấu phương thức này để xử lý yêu cầu HTTP POST với tuyến URL api/auth/register.
        public async Task<IActionResult> Register(RegisterDto registerDto) // Phương thức không đồng bộ để đăng ký người dùng, nhận vào RegisterDto chứa thông tin đăng ký.
        {
            await _userService.RegisterAsync(registerDto); // Gọi phương thức RegisterAsync từ IUserService để xử lý đăng ký người dùng.
            return Ok("User registered successfully"); // Trả về phản hồi HTTP 200 OK với thông báo đăng ký thành công.
        }

        [HttpPost("login")] // Đánh dấu phương thức này để xử lý yêu cầu HTTP POST với tuyến URL api/auth/login.
        public async Task<IActionResult> Login(LoginDto loginDto) // Phương thức không đồng bộ để xác thực người dùng, nhận vào LoginDto chứa thông tin đăng nhập.
        {
            var userDto = await _userService.AuthenticateAsync(loginDto); // Gọi phương thức AuthenticateAsync từ IUserService để xác thực người dùng.
            if (userDto == null) // Kiểm tra nếu kết quả xác thực là null (nghĩa là thông tin đăng nhập không hợp lệ).
                return Unauthorized("Invalid credentials"); // Trả về phản hồi HTTP 401 Unauthorized nếu xác thực thất bại.

            return Ok(userDto); // Trả về phản hồi HTTP 200 OK với thông tin người dùng nếu xác thực thành công.
        }
    }
}
