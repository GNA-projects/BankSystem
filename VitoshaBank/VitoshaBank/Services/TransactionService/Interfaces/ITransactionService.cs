using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.MessageModels;
using VitoshaBank.Data.Models;
using VitoshaBank.Data.ResponseModels;

namespace VitoshaBank.Services.TransactionService.Interfaces
{
    public interface ITransactionService
    {
        public Task<ActionResult<GetTransactionsResponseModel>> GetTransactionInfo(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel _messageModel);
        public  Task<ActionResult> CreateTransaction(ClaimsPrincipal currentUser, decimal amount, Transactions transaction, string senderType, string recieverType, string reason, BankSystemContext _context,   MessageModel _messageModel);
    }
}
