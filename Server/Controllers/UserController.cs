using Microsoft.AspNetCore.Mvc;
using QuintrixFullstack.Server.Data;
using QuintrixFullstack.Shared.Dto;

namespace QuintrixFullstack.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<List<UserInfoDto>>> GetUsers()
        {
            if (_context.Users is null)
            {
                return NotFound();
            }
            return await _context.Users.Select(u => new UserInfoDto() {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Roles = "NA",
                Created = u.Created })
                .ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<UserInfoDto>> GetUser(Guid id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return new UserInfoDto() {
                Id = id,
                Username = user.Username,
                Email = user.Email,
                Roles = "NA",
                Created = user.Created };
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserRegisterDto userDto)
        {
            var user = _context.Users?.FirstOrDefault(u => u.Id == id);
            if (user is null) return BadRequest();

            user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.PasswordHash = System.Text.Encoding.UTF8.GetBytes(userDto.Password);
            user.PasswordSalt = System.Text.Encoding.UTF8.GetBytes("FUCK");

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/User/Create
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Create")]
        public async Task<ActionResult<User>> CreateUser([FromBody] UserRegisterDto userDto)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'AppDbContext.Users'  is null.");
            }
            var user = new User() {
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = System.Text.Encoding.UTF8.GetBytes(userDto.Password),
                PasswordSalt = System.Text.Encoding.UTF8.GetBytes("FUCK")
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
