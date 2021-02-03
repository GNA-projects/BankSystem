using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.MessageModels;
using VitoshaBank.Data.Models;
using VitoshaBank.Data.ResponseModels;
using VitoshaBank.Services.DebitCardService.Interfaces;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;
using VitoshaBank.Services.Interfaces.UserService;
using VitoshaBank.Services.TransactionService.Interfaces;

namespace VitoshaBank.Services.BankAccountService.Interfaces
{
    public interface IBankAccountService
    {

        public Task<ActionResult<ChargeAccountResponseModel>> GetBankAccountInfo(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel messageModel);
        public Task<ActionResult<MessageModel>> DeleteBankAccount(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel messageModel);
        public Task<ActionResult<MessageModel>> SimulatePurchase(ChargeAccounts bankAccount, string product, ClaimsPrincipal currentUser, string username, decimal amount, string reciever, BankSystemContext _context, ITransactionService _transaction, MessageModel messageModel);
        public Task<ActionResult<MessageModel>> DepositMoney(ChargeAccounts bankAccount, ClaimsPrincipal currentUser, string username, decimal amount, BankSystemContext _context, ITransactionService _transactionService, MessageModel messageModel);
        public Task<ActionResult<MessageModel>> AddMoney(ChargeAccounts bankAccount, ClaimsPrincipal currentUser, string username, decimal amount, BankSystemContext _context, ITransactionService _transactionService, MessageModel messageModel);
        public Task<ActionResult<MessageModel>> CreateBankAccount(ClaimsPrincipal currentUser, string username, ChargeAccounts bankAccount, IIBANGeneratorService _IBAN, IBCryptPasswordHasherService _BCrypt, BankSystemContext _context, IDebitCardService _debitCardService, MessageModel messageModel);
        public Task<ActionResult<MessageModel>> Withdraw(ChargeAccounts bankAccount, ClaimsPrincipal currentUser, string username, decimal amount, string reciever, BankSystemContext _context, ITransactionService _transaction, MessageModel _messageModel);

    }
}
