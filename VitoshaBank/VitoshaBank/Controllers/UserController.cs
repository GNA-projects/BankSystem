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

        [HttpGet("users/{id}")]
        [Authorize]
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
        public async Task<ActionResult<BankSystemContext>> CreateUser(Users user)
        {
            var currentUser = HttpContext.User;
            string role = "";

            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                string userRole = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Roles").Value;
                role = userRole;
            }

            if (role == "Admin")
            {
                user.FirstName = "Dimo";
                user.LastName = "Dim";
                user.Username = "dimdim";
                user.Password = "12345";
                user.RegisterDate = DateTime.Now;
                user.BirthDate = DateTime.Now;
                user.IsAdmin = false;
                _context.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult LoginUser(Users userLogin)
        {
            ActionResult response = Unauthorized();

            var user = AuthenticateUser(userLogin);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(tokenString);
            }

            return response;
        }   
        private Users AuthenticateUser(Users userLogin)
        {
            var userAuthenticate = _context.Users.FirstOrDefault(x => x.Username == userLogin.Username);

            if (userAuthenticate == null)
            {
                return userAuthenticate;
            }
            else if (userLogin.Username == userAuthenticate.Username && userLogin.Password == userAuthenticate.Password)
            {
                return userAuthenticate;
            }
            return userAuthenticate;
        }
        private string GenerateJSONWebToken(Users userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var role = "";

            if (userInfo.IsAdmin == true)
            {
                role = "Admin";
            }
            else
            {
                role = "User";
            }

            var claims = new[] {
                         new Claim("Username", userInfo.Username),
                         new Claim("Password", userInfo.Password),
                         new Claim("Roles", role)
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
