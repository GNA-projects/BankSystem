using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;
using VitoshaBank.Data.RequestModels;
using VitoshaBank.Data.ResponseModels;
using VitoshaBank.Services.Interfaces;
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
        public UsersController(BankSystemContext context, ILogger<Users> logger, IConfiguration config, IBCryptPasswordHasherService BCrypt, IUsersService userService)
        {
            _context = context;
            _logger = logger;
            _config = config;
            _BCrypt = BCrypt;
            _userService = userService;
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            var currentUser = HttpContext.User;
            return await _userService.GetAllUsers(currentUser, _context);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            var currentUser = HttpContext.User;
            return await _userService.GetUser(currentUser, id, _context);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<BankSystemContext>> CreateUser(UserRequestModel user)
        {
            var currentUser = HttpContext.User;
            return await _userService.CreateUser(currentUser, user.User, _BCrypt, _context);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> LoginUser(UserRequestModel userLogin)
        {
            return await _userService.LoginUser(userLogin.User, _context, _BCrypt, _config);
        }

        [HttpPut("changepass")]
        [Authorize]
        public async Task<ActionResult> ChangePassword(UserRequestModel password)
        {
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _userService.ChangePassword(username, password.User.Password, _context, _BCrypt);
        }
        
        [HttpDelete("delete")]
        [Authorize]
        public async Task<ActionResult<Users>> DeleteUser(UserRequestModel username)
        {
            //username = "";
            var currentUser = HttpContext.User;
            return await _userService.DeleteUser(currentUser, username.Username, _context);
        }
    }
}
