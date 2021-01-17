using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.MessageModels;
using VitoshaBank.Data.Models;

namespace VitoshaBank.Services.Interfaces.UserService
{
    public interface IUsersService
    {

        public Task<ActionResult<IEnumerable<Users>>> GetAllUsers(ClaimsPrincipal currentUser, BankSystemContext _context, MessageModel responseMessage);
        public Task<ActionResult<Users>> GetUser(ClaimsPrincipal currentUser, int id, BankSystemContext _context, MessageModel responseMessage);
       
        public Task<ActionResult> LoginUser(Users userLogin, BankSystemContext _context, IBCryptPasswordHasherService _BCrypt, IConfiguration _config, MessageModel responseMessage);
       
        public Task<ActionResult<Users>> DeleteUser(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel responseMessage);
        
        public Task<ActionResult<BankSystemContext>> CreateUser(ClaimsPrincipal currentUser, Users user, IBCryptPasswordHasherService _BCrypt, BankSystemContext _context, MessageModel messageModel);
        
        public Task<ActionResult> ChangePassword(string username, string newPassword, BankSystemContext _context, IBCryptPasswordHasherService _BCrypt, MessageModel messageModel);
        
    }
}
