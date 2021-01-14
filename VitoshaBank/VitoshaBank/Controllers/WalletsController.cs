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
using VitoshaBank.Data.ResponseModels;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;
using VitoshaBank.Services.Interfaces.WalletService;

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
        private readonly IIBANGeneratorService _IBAN;
        public WalletsController(BankSystemContext context, ILogger<Users> logger, IConfiguration config, IWalletsService walletService, IIBANGeneratorService IBAN)
        {
            _context = context;
            _logger = logger;
            _config = config;
            _walletService = walletService;
            _IBAN = IBAN;
        }

        [HttpGet("get/{username}")]
        [Authorize]
        public async Task<ActionResult<WalletResponseModel>> GetWalletInfo(string username)
        {
            var currentUser = HttpContext.User;
            return await _walletService.GetWalletInfo(currentUser, username, _context);
        }

        [HttpPost("create/{username}")]
        [Authorize]
        public async Task<ActionResult> CreateWallet(Wallets wallet, string username)
        {
            var currentUser = HttpContext.User;
            return await _walletService.CreateWallet(currentUser, username, wallet,_IBAN, _context);
        }

        [HttpPut("deposit/{username}")]
        [Authorize]
        public async Task<ActionResult> DepositInWallet(Wallets wallet, decimal amount, string username)
        {
            amount = 0.50M;
            var currentUser = HttpContext.User;
            return await _walletService.DepositMoney(wallet, currentUser, username, amount, _context);
        }

        [HttpPut("purchase/{username}")]
        [Authorize]
        public async Task<ActionResult> PurchaseWithWallet(Wallets wallet, string product, decimal amount, string username)
        {
            amount = 10000;
            product = "Headphones";
            var currentUser = HttpContext.User;
            return await _walletService.SimulatePurchase(wallet, product, currentUser, username, amount, _context);
        }

        [HttpDelete("delete/{username}")]
        [Authorize]
        public async Task<ActionResult<Users>> DeleteWallet(string username)
        {
            var currentUser = HttpContext.User;
            return await _walletService.DeleteWallet(currentUser, username, _context);
        }
    }
}
