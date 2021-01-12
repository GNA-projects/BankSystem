using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;
using VitoshaBank.Data.ResponseModels;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;

namespace VitoshaBank.Services.Interfaces.WalletService
{
    public interface IWalletsService
    {
        public Task<ActionResult<WalletResponseModel>> GetWalletInfo(ClaimsPrincipal currentUser, string username, BankSystemContext _context);
        public Task<ActionResult> CreateWallet(ClaimsPrincipal currentUser, string username, Wallets wallet, IIBANGeneratorService _IBAN, BankSystemContext _context);
        public Task<ActionResult> DepositMoney(Wallets wallet, ClaimsPrincipal currentUser, string username, decimal amount, BankSystemContext _context);
        public Task<ActionResult> SimulatePurchase(Wallets wallet, string product, ClaimsPrincipal currentUser, string username, decimal amount, BankSystemContext _context);
        public Task<ActionResult<Users>> DeleteWallet(ClaimsPrincipal currentUser, string username, BankSystemContext _context);
    }
}
