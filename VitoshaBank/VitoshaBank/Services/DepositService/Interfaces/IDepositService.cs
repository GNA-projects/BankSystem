using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.MessageModels;
using VitoshaBank.Data.Models;
using VitoshaBank.Data.ResponseModels;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;
using VitoshaBank.Services.TransactionService.Interfaces;

namespace VitoshaBank.Services.DepositService
{
    public interface IDepositService
    {
        public Task<ActionResult<DepositResponseModel>> GetDepositInfo(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel _messageModel);
        public Task<ActionResult<MessageModel>> CreateDeposit(ClaimsPrincipal currentUser, string username, Deposits deposit, IIBANGeneratorService _IBAN, BankSystemContext _context, MessageModel _messageModel);
        public Task<ActionResult<MessageModel>> DeleteDeposit(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel _messageModel);
        public Task<ActionResult<MessageModel>> AddMoney(Deposits deposit, BankAccounts bankAccount, ClaimsPrincipal currentUser, string username, decimal amount, BankSystemContext _context, ITransactionService _transactionService, MessageModel _messageModel);
    }
}
