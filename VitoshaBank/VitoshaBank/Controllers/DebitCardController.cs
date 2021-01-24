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
        private readonly MessageModel _messageModel;
       

        public DebitCardController(BankSystemContext context, ILogger<Cards> logger, IDebitCardService debitCardService, IBankAccountService bankaccService, ITransactionService transactionService)
        {
            _context = context;
            _logger = logger;
            _debitCardService = debitCardService;
            _bankaccService = bankaccService;
            _messageModel = new MessageModel();
            _transactionService = transactionService;
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
            //amount = 0.50M;
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _debitCardService.DepositMoney(requestModel.Card.CardNumber, currentUser, _bankaccService, username, requestModel.Amount, _context,_transactionService, _messageModel);
        }

        [HttpPut("purchase")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> Purchase(DebitCardRequestModel requestModel)
        {

            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _debitCardService.SimulatePurchase(requestModel.Card.CardNumber, _bankaccService, requestModel.Product, currentUser, requestModel.Username, requestModel.Amount, requestModel.Reciever, _context, _transactionService, _messageModel);
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> DeleteDebitCard(DebitCardRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _debitCardService.DeleteDebitCard(currentUser, requestModel.Username, _context, _messageModel);
        }
    }
}
