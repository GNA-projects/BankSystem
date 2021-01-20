using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VitoshaBank.Data.MessageModels;
using VitoshaBank.Data.Models;
using VitoshaBank.Data.RequestModels;
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
        private readonly MessageModel _messageModel;
        public WalletsController(BankSystemContext context, ILogger<Wallets> logger, IWalletsService walletService, IIBANGeneratorService IBAN)
        {
            _context = context;
            _logger = logger;
            _walletService = walletService;
            _IBAN = IBAN;
            _messageModel = new MessageModel();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<WalletResponseModel>> GetWalletInfo()
        {
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _walletService.GetWalletInfo(currentUser, username, _context, _messageModel);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> CreateWallet(WalletRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _walletService.CreateWallet(currentUser, requestModel.Username, requestModel.Wallet, _IBAN, _context, _messageModel);
        }

        [HttpPut("deposit")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> DepositInWallet(WalletRequestModel requestModel)
        {
            //amount = 0.50M;
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _walletService.DepositMoney(requestModel.Wallet, currentUser, username, requestModel.Amount, _context, _messageModel);
        }

        [HttpPut("purchase")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> PurchaseWithWallet(WalletRequestModel requestModel)
        {
            //amount = 10000;
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _walletService.SimulatePurchase(requestModel.Wallet, requestModel.Product, currentUser, username, requestModel.Amount, _context, _messageModel);
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> DeleteWallet(WalletRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _walletService.DeleteWallet(currentUser, requestModel.Username, _context, _messageModel);
        }
    }
}
