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
using VitoshaBank.Services.Interfaces.UserService;
using VitoshaBank.Services.TransactionService.Interfaces;

namespace VitoshaBank.Services.Interfaces.WalletService
{
    public interface IWalletsService
    {
        public Task<ActionResult<WalletResponseModel>> GetWalletInfo(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel _messageModel);
        public Task<ActionResult<MessageModel>> CreateWallet(ClaimsPrincipal currentUser, string username, Wallets wallet, IIBANGeneratorService _IBAN, IBCryptPasswordHasherService _BCrypt, BankSystemContext _context, MessageModel _messageModel);
        public Task<ActionResult<MessageModel>> AddMoney(Wallets wallet, ClaimsPrincipal currentUser, string username, decimal amount, BankSystemContext _context, ITransactionService _transation, MessageModel _messageModel);
        public Task<ActionResult<MessageModel>> SimulatePurchase(Wallets wallet, string product, string reciever, ClaimsPrincipal currentUser, string username, decimal amount, BankSystemContext _context, ITransactionService _transation, IBCryptPasswordHasherService _BCrypt, MessageModel _messageModel);
        public Task<ActionResult<MessageModel>> DeleteWallet(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel _messageModel);
    }
}
