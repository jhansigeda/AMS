using Identity.API.Helpers;
using Identity.API.Models;
using Identity.API.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtHelper _jwtHelper;

        public AuthController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _jwtHelper = new JwtHelper(config);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            var user = _userService.Authenticate(login.Username, login.Password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = _jwtHelper.GenerateToken(user);
            return Ok(new { Token = token, Role = user.Role });
        }
    }
}
