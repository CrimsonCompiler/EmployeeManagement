using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration  _configuration;
        private readonly IUserRepository _userRepository;


        // Using DI to read JWT settings
        public AuthController(IConfiguration configuration, IUserRepository userRepository)
        {
               _configuration = configuration;
               _userRepository = userRepository;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
           // asking the repo
           var user = _userRepository.GetUserCredentials(login.Username, login.Password);

            if (user != null) 
            {
                var tokenString = GenerateJwtToken(user.Username, user.Role);
                return Ok(new {Token = tokenString});
            }

            return Unauthorized("Invalid Credentials");
        }


        private string GenerateJwtToken(string username, string role)
        {
            // step - 1 Setup secret key and algorithm ( signature )
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Claims / Payload - store user information
            var claims = new[] { 
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };



            // Making the token
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
