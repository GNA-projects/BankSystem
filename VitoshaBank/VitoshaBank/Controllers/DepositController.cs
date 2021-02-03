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
using VitoshaBank.Services.DepositPayentService;
using VitoshaBank.Services.DepositPayentService.Interfaces;
using VitoshaBank.Services.DepositService;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;
using VitoshaBank.Services.TransactionService.Interfaces;

namespace VitoshaBank.Controllers
{
    [Route("api/deposit")]
    [ApiController]
    public class DepositController : ControllerBase
    {
        private readonly BankSystemContext _context;
        private readonly ILogger<Deposits> _logger;
        private readonly IDepositService _depositService;
        private readonly IIBANGeneratorService _IBAN;
        private readonly ITransactionService _transactionService;
        private readonly IDividentPaymentService _dividentPayment;
        private readonly MessageModel _messageModel;
        public DepositController(BankSystemContext context, ILogger<Deposits> logger, IDepositService depositService, IIBANGeneratorService IBAN,ITransactionService transactionService, IDividentPaymentService dividentPayment)
        {
            _context = context;
            _logger = logger;
            _depositService = depositService;
            _IBAN = IBAN;
            _messageModel = new MessageModel();
            _transactionService = transactionService;
            _dividentPayment = dividentPayment;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<DepositResponseModel>> GetDepositInfo()
        {
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _depositService.GetDepositInfo(currentUser, username, _context, _messageModel);
        }


        [HttpPut("deposit")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> DepositMoney(DepositRequestModel requestModel)
        {
            //need from deposit(IBAN), BankAcc(IBAN),Username,Amount
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _depositService.AddMoney(requestModel.Deposit, requestModel.BankAccount, currentUser, username, requestModel.Amount, _context, _transactionService, _messageModel);
        }
        [HttpPut("withdraw")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> WithDrawMoney(DepositRequestModel requestModel)
        {
            //need from deposit(IBAN), BankAcc(IBAN),Username,Amount
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _depositService.WithdrawMoney(requestModel.Deposit, currentUser, username, requestModel.Amount, _context, _transactionService, _messageModel);
        }
        [HttpGet("check")]
        [Authorize]
        public async Task<ActionResult<DepositResponseModel>> GetDividentInfo()
        {
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _depositService.GetDividentInfo(currentUser, username, _context,_dividentPayment, _messageModel);
        }

    }
}
