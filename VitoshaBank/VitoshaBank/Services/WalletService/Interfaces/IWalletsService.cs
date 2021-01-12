using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;

namespace VitoshaBank.Services.Interfaces.WalletService
{
    public interface IWalletsService
    {
        public Task<ActionResult> CreateWallet(ClaimsPrincipal currentUser, string username, Wallets wallet, IIBANGeneratorService _IBAN, BankSystemContext _context);
        public Task<ActionResult<Users>> DeleteWallet(ClaimsPrincipal currentUser, string username, BankSystemContext _context);
    }
}
