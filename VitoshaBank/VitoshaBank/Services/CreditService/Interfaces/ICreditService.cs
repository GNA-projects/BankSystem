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

namespace VitoshaBank.Services.CreditService.Interfaces
{
   public  interface ICreditService
    {
        public Task<ActionResult<MessageModel>> CreateCredit(ClaimsPrincipal currentUser, string username, Credits credits,int period, IIBANGeneratorService _IBAN, BankSystemContext _context, MessageModel _messageModel);
        public Task<ActionResult<CreditResponseModel>> GetCreditInfo(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel _messageModel);
        public Task<ActionResult<MessageModel>> DepositMoney(Credits credit, ClaimsPrincipal currentUser, string username, decimal amount, BankSystemContext _context, MessageModel _messageModel);
        public Task<ActionResult<MessageModel>> DeleteCredit(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel _messageModel);
        public Task<ActionResult<MessageModel>> SimulatePurchase(Credits credit, string product, ClaimsPrincipal currentUser, string username, decimal amount, BankSystemContext _context, MessageModel _messageModel);
    }
}
