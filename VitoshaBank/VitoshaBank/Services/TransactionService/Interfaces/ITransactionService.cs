using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.MessageModels;
using VitoshaBank.Data.Models;

namespace VitoshaBank.Services.TransactionService.Interfaces
{
    public interface ITransactionService
    {
        public  Task<ActionResult> CreateTransaction(ClaimsPrincipal currentUser, decimal amount, Transactions transaction, string senderType, string recieverType, string reason, BankSystemContext _context,   MessageModel _messageModel);
    }
}
