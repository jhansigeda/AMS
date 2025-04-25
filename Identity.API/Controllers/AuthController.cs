using Identity.API.DTOs;
using Identity.API.Helpers;
using Identity.API.Models;
using Identity.API.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        //private readonly IUserService _userService;
        //private readonly JwtHelper _jwtHelper;

        //public AuthController(IUserService userService, IConfiguration config)
        //{
        //    _userService = userService;
        //    _jwtHelper = new JwtHelper(config);
        //}

        //[HttpPost("login")]
        //public IActionResult Login([FromBody] User login)
        //{
        //    var user = _userService.Authenticate(login.Username, login.Password);

        //    if (user == null)
        //        return Unauthorized("Invalid credentials");

        //    var token = _jwtHelper.GenerateToken(user);
        //    return Ok(new { Token = token, Role = user.Role });
        //}
               

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            if (!await _roleManager.RoleExistsAsync(dto.Role))
                return BadRequest("Invalid role");

            await _userManager.AddToRoleAsync(user, dto.Role);
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
                return Unauthorized("Invalid username or password.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                return Unauthorized("Invalid username or password.");

            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateJwtToken(user, roles.FirstOrDefault());

            return Ok(new AuthResponseDto
            {
                Token = token,
                Username = user.UserName,
                Role = roles.FirstOrDefault()
            });
        }

        private string GenerateJwtToken(ApplicationUser user, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(6),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
