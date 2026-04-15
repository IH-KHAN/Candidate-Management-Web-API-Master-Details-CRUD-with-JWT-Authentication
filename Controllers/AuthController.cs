using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Prac7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        public AuthController(IConfiguration config) => _config = config;
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto login)
        {
            if (login.userName == "admin" && login.password == "admin123")
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]!));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(expires: DateTime.Now.AddDays(1), signingCredentials: creds);
                return Ok(new {token=new JwtSecurityTokenHandler().WriteToken(token)});

            }
            return Unauthorized();
        }
        public class LoginDto
        {
            public string? userName { get; set; }
            public string? password { get; set; }
        }
    }
}
