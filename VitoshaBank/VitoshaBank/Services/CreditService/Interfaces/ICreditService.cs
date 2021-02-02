using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.MessageModels;
using VitoshaBank.Data.Models;
using VitoshaBank.Data.ResponseModels;
using VitoshaBank.Services.CreditPayOffService.Interfaces;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;
using VitoshaBank.Services.TransactionService.Interfaces;

namespace VitoshaBank.Services.CreditService.Interfaces
{
    public interface ICreditService
    {
        public Task<ActionResult<MessageModel>> CreateCredit(ClaimsPrincipal currentUser, string username, Credits credits, int period, IIBANGeneratorService _IBAN, BankSystemContext _context, MessageModel _messageModel);
        public Task<ActionResult<CreditResponseModel>> GetCreditInfo(ClaimsPrincipal currentUser, string username, ICreditPayOffService _payOffService, BankSystemContext _context, MessageModel _messageModel);
        public Task<ActionResult<MessageModel>> AddMoney(Credits credit, ClaimsPrincipal currentUser, string username, decimal amount, BankSystemContext _context, ITransactionService _transaction, MessageModel _messageModel);
        public Task<ActionResult<MessageModel>> DeleteCredit(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel _messageModel);
        public Task<ActionResult<MessageModel>> SimulatePurchase(Credits credit, string product, string reciever, ClaimsPrincipal currentUser, string username, decimal amount, BankSystemContext _context, ITransactionService _transactionService, MessageModel _messageModel);
        public Task<ActionResult<MessageModel>> Withdraw(Credits credit, ClaimsPrincipal currentUser, string username, decimal amount, string reciever, BankSystemContext _context, ITransactionService _transaction, MessageModel _messageModel);
     }
}
