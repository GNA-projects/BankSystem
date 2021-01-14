using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;
using VitoshaBank.Data.ResponseModels;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;

namespace VitoshaBank.Services.CreditService.Interfaces
{
   public  interface ICreditService
    {
        public Task<ActionResult> CreateCredit(ClaimsPrincipal currentUser, string username, Credits credits, IIBANGeneratorService _IBAN, BankSystemContext _context, ICalculateInterestService _interestDepositService);
        public Task<ActionResult<CreditResponseModel>> GetCreditInfo(ClaimsPrincipal currentUser, string username, BankSystemContext _context);
        public Task<ActionResult<Users>> DeleteCredit(ClaimsPrincipal currentUser, string username, BankSystemContext _context);
    }
}
