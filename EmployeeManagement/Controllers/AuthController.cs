using EmployeeManagement.Models;
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


        // Using DI to read JWT settings
        public AuthController(IConfiguration configuration)
        {
               _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if(login.Username == "admin" && login.Password == "1234")
            {
                var tokenString = GenerateJwtToken(login.Username);
                return Ok(new { Token = tokenString });
            }

            return Unauthorized("Invalid Credentials");
        }


        private string GenerateJwtToken(string username)
        {
            // step - 1 Setup secret key and algorithm ( signature )
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Claims / Payload - store user information
            var claims = new[] { 
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
        }


    }
}
