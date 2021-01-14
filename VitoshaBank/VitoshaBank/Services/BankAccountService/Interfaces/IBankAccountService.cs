using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;
using VitoshaBank.Services.DebitCardService.Interfaces;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;

namespace VitoshaBank.Services.BankAccountService.Interfaces
{
    public interface IBankAccountService
    {
        public Task<ActionResult> CreateBankAccount(ClaimsPrincipal currentUser, string username, BankAccounts bankAccounts, IIBANGeneratorService _IBAN, BankSystemContext _context, IDebitCardService _debitCardService);
        public Task<ActionResult<Users>> DeleteBankAccount(ClaimsPrincipal currentUser, string username, BankSystemContext _context);
    }
}
