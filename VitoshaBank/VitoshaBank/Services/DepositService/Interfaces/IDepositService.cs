using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;

namespace VitoshaBank.Services.DepositService
{
    interface IDepositService
    {
        public Task<ActionResult> CreateDeposit(ClaimsPrincipal currentUser, string username, Deposits deposit, IIBANGeneratorService _IBAN, BankSystemContext _context);
        public Task<ActionResult<Users>> DeleteDeposit(ClaimsPrincipal currentUser, string username, BankSystemContext _context);
    }
}
