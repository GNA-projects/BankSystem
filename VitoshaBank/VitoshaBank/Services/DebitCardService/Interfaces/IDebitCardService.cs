using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;

namespace VitoshaBank.Services.DebitCardService.Interfaces
{
    interface IDebitCardService
    {
        public Task<ActionResult> CreateDebitCard(ClaimsPrincipal currentUser, string username, BankAccounts bankAccount, BankSystemContext _context, Cards card);
        public Task<ActionResult<Users>> DeleteDebitCard(ClaimsPrincipal currentUser, string username, BankSystemContext _context);
    }
}
