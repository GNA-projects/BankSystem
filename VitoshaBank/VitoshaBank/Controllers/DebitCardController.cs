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
using VitoshaBank.Services.Interfaces.UserService;
using VitoshaBank.Services.TransactionService.Interfaces;

namespace VitoshaBank.Controllers
{
    [Route("api/card")]
    [ApiController]
    public class DebitCardController : ControllerBase
    {
        private readonly BankSystemContext _context;
        private readonly ILogger<Cards> _logger;
        private readonly IDebitCardService _debitCardService;
        private readonly IBankAccountService _bankaccService;
        private readonly ITransactionService _transactionService;
        private readonly IBCryptPasswordHasherService _BCrypt;
        private readonly MessageModel _messageModel;


        public DebitCardController(BankSystemContext context, ILogger<Cards> logger, IDebitCardService debitCardService, IBankAccountService bankaccService, ITransactionService transactionService, IBCryptPasswordHasherService BCrypt)
        {
            _context = context;
            _logger = logger;
            _debitCardService = debitCardService;
            _bankaccService = bankaccService;
            _messageModel = new MessageModel();
            _transactionService = transactionService;
            _BCrypt = BCrypt;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<DebitCardResponseModel>> GetDebitCardInfo()
        {
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _debitCardService.GetDebitCardInfo(currentUser, username, _context, _messageModel);
        }

        [HttpPut("deposit")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> Deposit(DebitCardRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _debitCardService.AddMoney(requestModel.Card.CardNumber, requestModel.Card.Cvv, requestModel.Card.CardExiprationDate, currentUser, _bankaccService, _BCrypt,username, requestModel.Amount, _context, _transactionService, _messageModel);
        }

        [HttpPut("purchase")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> Purchase(DebitCardRequestModel requestModel)
        {

            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _debitCardService.SimulatePurchase(requestModel.Card.CardNumber, requestModel.Card.Cvv, requestModel.Card.CardExiprationDate, _bankaccService, _BCrypt,requestModel.Product, currentUser, username, requestModel.Amount, requestModel.Reciever, _context, _transactionService, _messageModel);
        }

        [HttpPut("withdraw")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> Withdraw(DebitCardRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _debitCardService.Withdraw(requestModel.Card.CardNumber, requestModel.Card.Cvv, requestModel.Card.CardExiprationDate, _bankaccService, _BCrypt,currentUser, username, requestModel.Amount, requestModel.Reciever, _context, _transactionService, _messageModel);
        }


    }
}
