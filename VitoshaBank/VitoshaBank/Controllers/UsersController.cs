using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VitoshaBank.Data.MessageModels;
using VitoshaBank.Data.Models;
using VitoshaBank.Data.RequestModels;
using VitoshaBank.Services.Interfaces.UserService;

namespace VitoshaBank.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly BankSystemContext _context;
        private readonly IBCryptPasswordHasherService _BCrypt;
        private readonly ILogger<Users> _logger;
        private readonly IConfiguration _config;
        private readonly IUsersService _userService;
        private readonly MessageModel _responseMessage;
        public UsersController(BankSystemContext context, ILogger<Users> logger, IConfiguration config, IBCryptPasswordHasherService BCrypt, IUsersService userService)
        {
            _context = context;
            _logger = logger;
            _config = config;
            _BCrypt = BCrypt;
            _userService = userService;
            _responseMessage = new MessageModel();
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            //return all users
            var currentUser = HttpContext.User;
            return await _userService.GetAllUsers(currentUser, _context, _responseMessage);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<MessageModel>> LoginUser(UserRequestModel requestModel)
        {
            return await _userService.LoginUser(requestModel.User, _context, _BCrypt, _config, _responseMessage);
        }

        [HttpPut("changepass")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> ChangePassword(UserRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _userService.ChangePassword(username, requestModel.User.Password, _context, _BCrypt, _responseMessage);
        }

        [HttpGet("activateaccount/{id}")]
        public async Task<ActionResult<MessageModel>> VeryfiyUserAccount(string id)
        {
            return await _userService.VerifyAccount(id, _context, _responseMessage);
        }
        [HttpGet("username")]
        [Authorize]
        public async  Task<ActionResult<MessageModel>> GetUsername()
        {
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _userService.GetUsername(username, _context, _responseMessage);
        }
        [HttpGet("authenticate")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> AutihentivcateUser()
        {

            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _userService.AdminCheck(username, _context, _responseMessage);
        }

    }
}
