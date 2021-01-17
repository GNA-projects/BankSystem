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
using VitoshaBank.Data.MessageModels;
using VitoshaBank.Data.Models;
using VitoshaBank.Data.ResponseModels;
using VitoshaBank.Services.Interfaces.UserService;

namespace VitoshaBank.Services.UserService
{
    public class UsersService : ControllerBase, IUsersService
    {
        public async Task<ActionResult<IEnumerable<Users>>> GetAllUsers(ClaimsPrincipal currentUser, BankSystemContext _context, MessageModel responseMessage)
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
                responseMessage.Message = "You are not authorized to do such actions!";
                return StatusCode(403, responseMessage);
            }
        }

        public async Task<ActionResult<Users>> GetUser(ClaimsPrincipal currentUser, int id, BankSystemContext _context, MessageModel responseMessage)
        {
            string role = "";

            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                string userRole = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Roles").Value;
                role = userRole;
            }

            if (role == "Admin")
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    responseMessage.Message = "User not found";
                    return StatusCode(404, responseMessage);
                }

                return StatusCode(200, user);
            }
            else
            {
                responseMessage.Message = "You are not authorized to do such actions";
                return StatusCode(403, responseMessage);
            }
        }

        public async Task<ActionResult<BankSystemContext>> CreateUser(ClaimsPrincipal currentUser, Users user, IBCryptPasswordHasherService _BCrypt, BankSystemContext _context, MessageModel responseMessage)
        {
            string role = "";

            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                string userRole = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Roles").Value;
                role = userRole;
            }

            if (role == "Admin")
            {
                UserResponseModel userResponseModel = new UserResponseModel();
                if (user.FirstName.Length < 1)
                {

                    responseMessage.Message = "First name cannot be less than 1 symbol";
                    return StatusCode(400, responseMessage);

                }
                if (user.LastName.Length < 1)
                {

                    responseMessage.Message = "Last name cannot be less than 1 symbol";
                    return StatusCode(400, responseMessage);

                }
                if (user.Email.Length < 6)
                {

                    responseMessage.Message = "Email cannot be less than 1 symbol";
                    return StatusCode(400, responseMessage);

                }
                userResponseModel.FirstName = user.FirstName;
                userResponseModel.LastName = user.LastName;
                userResponseModel.Email = user.Email;
                if (user.Username.Length < 6)
                {
                    responseMessage.Message = "Username cannot be less than 6 symbols";
                    return StatusCode(400, responseMessage);
                }
                userResponseModel.Username = user.Username;
                if (user.Password.Length < 6)
                {
                    responseMessage.Message = "Password cannot be less than 6 symbols";
                    return StatusCode(400, responseMessage);
                }
                user.Password = _BCrypt.HashPassword(user.Password);
                user.RegisterDate = DateTime.Now;
                if ((user.RegisterDate - user.BirthDate).TotalDays < 6570)
                {
                    responseMessage.Message = "User cannot be less than 18 years old";
                    return StatusCode(400, responseMessage);
                }
                user.BirthDate = DateTime.Now;
                _context.Add(user);
                await _context.SaveChangesAsync();
                responseMessage.Message = $"User {user.Username} created succesfully!";
                return StatusCode(201, responseMessage);
            }
            else
            {
                responseMessage.Message = "You are not autorized to do such action!";
                return StatusCode(403, responseMessage);
            }
        }

        public async Task<ActionResult> LoginUser(Users userLogin, BankSystemContext _context, IBCryptPasswordHasherService _BCrypt, IConfiguration _config, MessageModel responseMessage)
        {
            responseMessage.Message = "You are not authorized to do such actions!";
            ActionResult response = StatusCode(403, responseMessage);

            var user = await AuthenticateUser(userLogin, _context, _BCrypt);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user, _config);
                responseMessage.Message = tokenString;
                response = StatusCode(200, responseMessage);
            }

            return response;
        }

        public async Task<ActionResult> ChangePassword(string username, string newPassword, BankSystemContext _context, IBCryptPasswordHasherService _BCrypt, MessageModel responseMessage)
        {
            var userAuthenticate = _context.Users.FirstOrDefault(x => x.Username == username);

            if (userAuthenticate != null)
            {
                if (newPassword == null || newPassword == "")
                {
                    responseMessage.Message = "Password cannot be null";
                    return StatusCode(400, responseMessage);
                }
                else if (newPassword.Length < 6)
                {
                    responseMessage.Message = "Password cannot be less than 6 symbols";
                    return StatusCode(400, responseMessage);

                }

                userAuthenticate.Password = _BCrypt.HashPassword(newPassword);
                await _context.SaveChangesAsync();

                responseMessage.Message = "Password changed successfully!";
                return StatusCode(200, responseMessage);
            }
            else
            {
                responseMessage.Message = "User not found!";
                return StatusCode(404, responseMessage);
            }
        }

        public async Task<ActionResult<Users>> DeleteUser(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel responseMessage)
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
                    responseMessage.Message = "User not found";
                    return StatusCode(404, responseMessage);
                }
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                responseMessage.Message = $"Succsesfully deleted user {user.Username}";
                return StatusCode(200, responseMessage);
            }
            else
            {
                responseMessage.Message = "You are not authorized to do such actions";
                return StatusCode(403, responseMessage);
            }
        }
        private async Task<Users> AuthenticateUser(Users userLogin, BankSystemContext _context, IBCryptPasswordHasherService _BCrypt)
        {
            var userAuthenticateUsername = await _context.Users.FirstOrDefaultAsync(x => x.Username == userLogin.Username);
            var userAuthenticateEmail = await _context.Users.FirstOrDefaultAsync(x => x.Email == userLogin.Email);
            if (userAuthenticateUsername == null && userAuthenticateEmail == null)
            {
                return null;
            }
            else if (userAuthenticateEmail == null)
            {
                if ((userLogin.Username == userAuthenticateUsername.Username && _BCrypt.Authenticate(userLogin, userAuthenticateUsername) == true))
                {
                    return userAuthenticateUsername;
                }
                return null;
            }
            else if( userAuthenticateUsername == null)
            {
                if ((userLogin.Email == userAuthenticateEmail.Email && _BCrypt.Authenticate(userLogin, userAuthenticateEmail) == true))
                {
                    return userAuthenticateEmail;
                }
                return null;
            }
            return null;
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
