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
using VitoshaBank.Services.BankAccountService.Interfaces;
using VitoshaBank.Services.DebitCardService.Interfaces;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;

namespace VitoshaBank.Controllers
{
    [Route("api/bankaccount")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly BankSystemContext _context;
        private readonly ILogger<BankAccounts> _logger;
        private readonly IBankAccountService _bankAccountService;
        private readonly IDebitCardService _debitCardService;
        private readonly IIBANGeneratorService _IBAN;
        private readonly MessageModel _messageModel;

        public BankAccountController(BankSystemContext context, ILogger<BankAccounts> logger, IBankAccountService bankAccountService, IIBANGeneratorService IBAN, IDebitCardService debitCardService)
        {
            _context = context;
            _logger = logger;
            _bankAccountService = bankAccountService;
            _debitCardService = debitCardService;
            _IBAN = IBAN;
            _messageModel = new MessageModel();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<BankAccountResponseModel>> GetBankAccountInfo()
        {
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _bankAccountService.GetBankAccountInfo(currentUser, username, _context, _messageModel);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult> CreateBankAccount(BankAccountRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _bankAccountService.CreateBankAccount(currentUser, requestModel.Username, requestModel.BankAccount,_IBAN, _context, _debitCardService, _messageModel);
        }
        [HttpPut("deposit")]
        [Authorize]
        public async Task<ActionResult> DepositInBankAcc(BankAccountRequestModel requestModel)
        {
            //amount = 0.50M;
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _bankAccountService.DepositMoney(requestModel.BankAccount, currentUser, username, requestModel.Amount, _context,_messageModel);
        }

        [HttpPut("purchase")]
        [Authorize]
        public async Task<ActionResult> PurchaseWithWallet(BankAccountRequestModel requestModel)
        {
            
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _bankAccountService.SimulatePurchase(requestModel.BankAccount, requestModel.Product, currentUser, username, requestModel.Amount, requestModel.Reciever, _context,_messageModel);
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<ActionResult<Users>> DeleteBankAccount(BankAccountRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _bankAccountService.DeleteBankAccount(currentUser, requestModel.Username, _context, _messageModel);
        }
    }
}
