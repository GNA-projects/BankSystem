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
using VitoshaBank.Data.ResponseModels;
using VitoshaBank.Services.TransactionService.Interfaces;

namespace VitoshaBank.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly BankSystemContext _context;
        private readonly ILogger<Transactions> _logger;
        private readonly ITransactionService _transactionService;
        private readonly MessageModel _messageModel;

        public TransactionsController(BankSystemContext context, ILogger<Transactions> logger, ITransactionService transactionService)
        {
            _context = context;
            _logger = logger;
            _transactionService = transactionService;
            _messageModel = new MessageModel();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<GetTransactionsResponseModel>> GetTransactionsInfo()
        {
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _transactionService.GetTransactionInfo(currentUser, username, _context, _messageModel);
        }
    }
}
