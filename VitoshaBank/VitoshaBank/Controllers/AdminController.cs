using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VitoshaBank.Data.MessageModels;
using VitoshaBank.Data.Models;
using VitoshaBank.Data.RequestModels;
using VitoshaBank.Services.BankAccountService.Interfaces;
using VitoshaBank.Services.DebitCardService.Interfaces;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;
using VitoshaBank.Services.TransactionService.Interfaces;

namespace VitoshaBank.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly BankSystemContext _context;
        private readonly ILogger<BankAccounts> _logger;
        private readonly IBankAccountService _bankAccountService;
        private readonly IDebitCardService _debitCardService;
        private readonly IIBANGeneratorService _IBAN;
        private readonly ITransactionService _transactionService;
        private readonly MessageModel _messageModel;

        public AdminController(BankSystemContext context, ILogger<BankAccounts> logger, IBankAccountService bankAccountService, IIBANGeneratorService IBAN, IDebitCardService debitCardService, ITransactionService transactionService)
        {
            _context = context;
            _logger = logger;
            _bankAccountService = bankAccountService;
            _debitCardService = debitCardService;
            _IBAN = IBAN;
            _transactionService = transactionService;
            _messageModel = new MessageModel();
        }

        [HttpPost("create/bankaccount")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> CreateBankAccount(BankAccountRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _bankAccountService.CreateBankAccount(currentUser, requestModel.Username, requestModel.BankAccount, _IBAN, _context, _debitCardService, _messageModel);
        }

        [HttpPut("addmoney/bankaccount")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> AddMoneyInBankAccount(BankAccountRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _bankAccountService.AddMoney(requestModel.BankAccount, currentUser, requestModel.Username, requestModel.Amount, _context, _transactionService, _messageModel);
        }
        [HttpDelete("delete/bankaccount")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> DeleteBankAccount(BankAccountRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _bankAccountService.DeleteBankAccount(currentUser, requestModel.Username, _context, _messageModel);
        }

    }
}
