using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;

namespace VitoshaBank.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly BankSystemContext _context;
        private readonly ILogger<Users> _logger;
        private readonly IConfiguration _config;
        public UserController(BankSystemContext context, ILogger<Users> logger, IConfiguration config)
        {
            _context = context;
            _logger = logger;
            _config = config;
        }

        [HttpGet("users")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<BankSystemContext>> PostUser(Users user)
        {
            user.FirstName = "Ni666k";
            user.LastName = "dsddadadada";
            user.Username = "66666666";
            user.Password = "666666";
            user.RegisterDate = DateTime.Now;
            user.BirthDate = DateTime.Now;
            _context.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult LoginPerson(Users userLogin)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(userLogin);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(tokenString);
            }

            return response;
        }

        [HttpGet]
        [Authorize]
        public IActionResult AdminGet()
        {
            var currentUser = HttpContext.User;
            string name = "";

            if (currentUser.HasClaim(c => c.Type == "Username"))
            {
                string userName = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
                name = userName;
            }
            var userAuthenticate = _context.Users.Find(name);

            if (name == userAuthenticate.Username)
            {
                return Ok(name);
            }
            else
            {
                return Unauthorized();
            }
        }

        private Users AuthenticateUser(Users userLogin)
        {
            Users user = null;
            var userAuthenticate =  _context.Users.FirstOrDefault(x => x.Username == userLogin.Username);

            if (userLogin.Username == userAuthenticate.Username && userLogin.Password == userAuthenticate.Password)
            {
                user = new Users { Username = userLogin.Username, Password = userLogin.Password };
            }
            return user;
        }
        private string GenerateJSONWebToken(Users userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                         new Claim("Username", userInfo.Username),
                         new Claim("Password", userInfo.Password),
                                };

            var token = new JwtSecurityToken(
                null,
                null,
                claims,
                null,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
