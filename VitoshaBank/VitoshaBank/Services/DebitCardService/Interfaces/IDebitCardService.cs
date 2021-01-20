using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.MessageModels;
using VitoshaBank.Data.Models;
using VitoshaBank.Data.ResponseModels;
using VitoshaBank.Services.BankAccountService.Interfaces;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;

namespace VitoshaBank.Services.DebitCardService.Interfaces
{
    public interface IDebitCardService
    {
        public Task<ActionResult<DebitCardResponseModel>> GetDebitCardInfo(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel _messageModel);
        public Task<ActionResult<MessageModel>> CreateDebitCard(ClaimsPrincipal currentUser, string username, BankAccounts bankAccount, BankSystemContext _context, Cards card, MessageModel _messageModel);
        public  Task<ActionResult<MessageModel>> DepositMoney(string cardNumber, ClaimsPrincipal currentUser, IBankAccountService _bankaccService, string username, decimal amount, BankSystemContext _context, MessageModel messageModel);
        public  Task<ActionResult<MessageModel>> SimulatePurchase(string cardNumber, IBankAccountService _bankaccService, string product, ClaimsPrincipal currentUser, string username, decimal amount, string reciever, BankSystemContext _context, MessageModel messageModel);
        public Task<ActionResult<MessageModel>> DeleteDebitCard(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel _messageModel);
    }
}
