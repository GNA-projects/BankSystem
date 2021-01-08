using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;

namespace VitoshaBank.Services.Interfaces
{
    public interface IWalletsService
    {
        public Task<ActionResult> CreateWallet(ClaimsPrincipal currentUser, string username, Wallets wallet, BankSystemContext _context);
        public Task<ActionResult<Users>> DeleteWallet(ClaimsPrincipal currentUser, string username, BankSystemContext _context);
    }
}
