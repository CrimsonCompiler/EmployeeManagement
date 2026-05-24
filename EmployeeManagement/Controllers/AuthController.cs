using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

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



    }
}
