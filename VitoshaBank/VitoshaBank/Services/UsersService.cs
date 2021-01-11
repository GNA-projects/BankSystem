using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;
using VitoshaBank.Services.Interfaces;

namespace VitoshaBank.Services
{
    public class UsersService : ControllerBase, IUsersService
    {
        public async Task<ActionResult<IEnumerable<Users>>> GetAllUsers(ClaimsPrincipal currentUser, BankSystemContext _context)
        {
            string role = "";

            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                string userRole = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Roles").Value;
                role = userRole;
            }

            if (role == "Admin")
            {
                return await _context.Users.ToListAsync();
            }
            else
            {
                return Unauthorized();
            }
        }

        public async Task<ActionResult<Users>> GetUser(ClaimsPrincipal currentUser, int id, BankSystemContext _context)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        public async Task<ActionResult<BankSystemContext>>CreateUser(ClaimsPrincipal currentUser, Users user, IBCryptPasswordHasherService _BCrypt, BankSystemContext _context)
        {
            string role = "";

            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                string userRole = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Roles").Value;
                role = userRole;
            }

            if (role == "Admin")
            {
                user.Password = _BCrypt.HashPassword(user.Password);
                user.RegisterDate = DateTime.Now;
                _context.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }
            else
            {
                return Unauthorized();
            }
        }

        public async Task<ActionResult> LoginUser(Users userLogin, BankSystemContext _context, IBCryptPasswordHasherService _BCrypt, IConfiguration _config)
        {
            ActionResult response = Unauthorized();

            var user = await AuthenticateUser(userLogin, _context, _BCrypt);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user, _config);
                response = Ok(tokenString);
            }

            return response;
        }

        public async Task<ActionResult> ChangePassword(Users user, BankSystemContext _context, IBCryptPasswordHasherService _BCrypt)
        {
            var userAuthenticate = _context.Users.FirstOrDefault(x => x.Username == user.Username);

            if (userAuthenticate != null)
            {
                userAuthenticate.Password = _BCrypt.HashPassword(user.Password);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(user.Username == userAuthenticate.Username))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return BadRequest();
            }

            return NoContent();
        }

        public async Task<ActionResult<Users>> DeleteUser(ClaimsPrincipal currentUser, string username, BankSystemContext _context)
        {
            string role = "";

            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                string userRole = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Roles").Value;
                role = userRole;
            }

            if (role == "Admin")
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
                if (user == null)
                {
                    return NotFound();
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return Ok(user);
            }
            else
            {
                return Unauthorized();
            }
        }
        private async Task<Users> AuthenticateUser(Users userLogin, BankSystemContext _context, IBCryptPasswordHasherService _BCrypt)
        {
            var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == userLogin.Username);

            if (userAuthenticate == null)
            {
                return userAuthenticate;
            }
            else if (userLogin.Username == userAuthenticate.Username && _BCrypt.Authenticate(userLogin, userAuthenticate) == true)
            {
                return userAuthenticate;
            }
            userAuthenticate = null;
            return userAuthenticate;
        }
        private string GenerateJSONWebToken(Users userInfo, IConfiguration _config)
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
