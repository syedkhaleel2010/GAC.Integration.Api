using GAC.Integration.Domain.Dto;
using GAC.Integration.Domain.Entities;
using GAC.Integration.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GAC.Integration.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ServiceControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginController> _logger;
        private readonly IUserService _userService;

        public LoginController(IConfiguration configuration,ILogger<LoginController> logger,IUserService userService) : base(logger)
        {
            _configuration = configuration;
            _logger = logger;
            _userService = userService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto model)
        {
            

            var userValid = await _userService.UserLogin(new User() { UserName = model.Username, Password = model.Password});
            if (userValid)
            {
                var claims = new[]
                {
            new Claim(ClaimTypes.Name, model.Username)
            };
                var _key = _configuration.GetValue<string>("Jwt:Key");
                var _Issuer = _configuration.GetValue<string>("Jwt:Issuer");

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _Issuer,
                    audience: null,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return OkServiceResponse(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            else
            {
                return BadRequest("Either username or password is incorrect");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            return OkServiceResponse(await _userService.CreateUser(model));
        }
    }
}
