using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;
using VitoshaBank.Services.Interfaces;

namespace VitoshaBank.Controllers
{
    [ApiController]
    [Route("api/wallets")]
    public class WalletsController : ControllerBase
    {
        private readonly BankSystemContext _context;
        private readonly ILogger<Users> _logger;
        private readonly IConfiguration _config;
        private readonly IWalletsService _walletService;
        public WalletsController(BankSystemContext context, ILogger<Users> logger, IConfiguration config, IWalletsService walletService)
        {
            _context = context;
            _logger = logger;
            _config = config;
            _walletService = walletService;
        }

        [HttpPost("create/{username}")]
        [Authorize]
        public async Task<ActionResult> CreateWallet(Wallets wallet, string username)
        {
            var currentUser = HttpContext.User;
            return await _walletService.CreateWallet(currentUser, username, wallet, _context);
        }

        [HttpDelete("delete/{username}")]
        [Authorize]
        public async Task<ActionResult<Users>> DeleteUser(string username)
        {
            var currentUser = HttpContext.User;
            return await _walletService.DeleteWallet(currentUser, username, _context);
        }
    }
}
