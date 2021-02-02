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
using VitoshaBank.Services.TransactionService.Interfaces;

namespace VitoshaBank.Controllers
{
    [Route("api/charge")]
    [ApiController]
    public class ChargeAccountController : ControllerBase
    {
        private readonly BankSystemContext _context;
        private readonly ILogger<ChargeAccounts> _logger;
        private readonly IBankAccountService _bankAccountService;
        private readonly IDebitCardService _debitCardService;
        private readonly IIBANGeneratorService _IBAN;
        private readonly ITransactionService _transactionService;
        private readonly MessageModel _messageModel;

        public ChargeAccountController(BankSystemContext context, ILogger<ChargeAccounts> logger, IBankAccountService bankAccountService, IIBANGeneratorService IBAN, IDebitCardService debitCardService, ITransactionService transactionService)
        {
            _context = context;
            _logger = logger;
            _bankAccountService = bankAccountService;
            _debitCardService = debitCardService;
            _IBAN = IBAN;
            _transactionService = transactionService;
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


        [HttpPut("fromdeposit")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> DepositInBankAcc(BankAccountRequestModel requestModel)
        {
            //amount = 0.50M;
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _bankAccountService.DepositMoney(requestModel.BankAccount, currentUser, username, requestModel.Amount, _context,_transactionService, _messageModel);
        }

        [HttpPut("purchase")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> Purchase(BankAccountRequestModel requestModel)
        {
            
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _bankAccountService.SimulatePurchase(requestModel.BankAccount, requestModel.Product, currentUser, username, requestModel.Amount, requestModel.Reciever, _context, _transactionService, _messageModel);
        }

        [HttpPut("withdraw")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> Withdraw(BankAccountRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _bankAccountService.Withdraw(requestModel.BankAccount, currentUser, username, requestModel.Amount, requestModel.Reciever, _context, _transactionService, _messageModel);
        }


    }
}
