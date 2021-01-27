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
        private readonly MessageModel _messageModel;
        public DepositController(BankSystemContext context, ILogger<Deposits> logger, IDepositService depositService, IIBANGeneratorService IBAN,ITransactionService transactionService)
        {
            _context = context;
            _logger = logger;
            _depositService = depositService;
            _IBAN = IBAN;
            _messageModel = new MessageModel();
            _transactionService = transactionService;
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
            return await _depositService.AddMoney(requestModel.Deposit, requestModel.BankAccount, currentUser, requestModel.Username, requestModel.Amount, _context, _transactionService, _messageModel);
        } 


    }
}
