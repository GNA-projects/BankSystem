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
        private readonly ILogger<Wallets> _logger;
        private readonly IWalletsService _walletService;
        private readonly IIBANGeneratorService _IBAN;
        public WalletsController(BankSystemContext context, ILogger<Wallets> logger, IWalletsService walletService, IIBANGeneratorService IBAN)
        {
            _context = context;
            _logger = logger;
            _walletService = walletService;
            _IBAN = IBAN;
        }

        [HttpGet("getwallet")]
        [Authorize]
        public async Task<ActionResult<WalletResponseModel>> GetWalletInfo()
        {
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _walletService.GetWalletInfo(currentUser, username, _context);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult> CreateWallet(Wallets wallet, string username)
        {
            var currentUser = HttpContext.User;
            return await _walletService.CreateWallet(currentUser, username, wallet,_IBAN, _context);
        }

        [HttpPut("deposit")]
        [Authorize]
        public async Task<ActionResult> DepositInWallet(Wallets wallet, decimal amount)
        {
            //amount = 0.50M;
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _walletService.DepositMoney(wallet, currentUser, username, amount, _context);
        }

        [HttpPut("purchase")]
        [Authorize]
        public async Task<ActionResult> PurchaseWithWallet(Wallets wallet, string product, decimal amount)
        {
            //amount = 10000;
            product = "Headphones";
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _walletService.SimulatePurchase(wallet, product, currentUser, username, amount, _context);
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<ActionResult<Users>> DeleteWallet(UserResponseModel username)
        {
            var currentUser = HttpContext.User;
            return await _walletService.DeleteWallet(currentUser, username.Username, _context);
        }
    }
}
