using Application_Temp.DTOs;
using Application_Temp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UJUTN_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            await _userService.RegisterAsync(registerDto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var userDto = await _userService.AuthenticateAsync(loginDto);
            if (userDto == null)
                return Unauthorized();

            return Ok(userDto);
        }

        [HttpPost("gettaikhoan")]
        [AllowAnonymous]
        public async Task<IActionResult> gettaikhoan()
        {
           var user= await _userService.gettaikhoan();
            return Ok(user);
        }
    }
}
