using Api_JewelryStore.LoginUserModel;
using Api_JewelryStore.Models;
//using Api_JewelryStore.Services; // Убедитесь, что это пространство имен существует
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Api_JewelryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DiplomDb3Context _context;

        public LoginController(DiplomDb3Context context)
        {
            _context = context;
        }
        // GET: api/users

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            var user = await _context.Users.ToListAsync();

            if (user == null || !user.Any())
            {
                return NotFound("No materials found.");
            }

            return Ok(user);
        }

        // POST: api/login/register
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Проверка на существующий email
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == registerDto.Email);
            if (existingUser != null)
            {
                return Conflict("User with this email already exists.");
            }

            // Создание нового пользователя
            var newUser = new User
            {
                Email = registerDto.Email,
                Password = registerDto.Password,
                IsAdmin = false,
                Money = 0
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }



    }
}