using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using WpfApp17.Model;
using WebApplication1.Classes;
using System.Text;
using WebApplication1.Model;
using System.Net.NetworkInformation;
using Newtonsoft.Json.Linq;

namespace WpfApp17.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AvtoContext _context;

        public AuthController(AvtoContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _context.Users.AnyAsync(u => u.Login == loginModel.Login))
                return Conflict("Пользователь с таким логином уже существует");

            var user = new User
            {
                Login = loginModel.Login,
                Password = HashPassword.CreateHash(loginModel.Password),
                RoleId = "Client",
                Email = loginModel.Email
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Login == loginModel.Login);

            if (user == null || !HashPassword.VerifyPassword(loginModel.Password, user.Password))
                return Unauthorized("Неверный логин или пароль");

            var token = GenerateToken(user.Login);
            user.Token = token;
            await _context.SaveChangesAsync();

            return Ok(new { Token = token });
        }

        private string GenerateToken(string login)
        {
            var token = $"{login}:{Guid.NewGuid()}";
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
        }
    }
}
