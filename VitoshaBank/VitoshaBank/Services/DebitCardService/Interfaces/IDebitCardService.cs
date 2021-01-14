using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;
using VitoshaBank.Data.ResponseModels;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;

namespace VitoshaBank.Services.DebitCardService.Interfaces
{
    public interface IDebitCardService
    {
        public Task<ActionResult<DebitCardResponseModel>> GetDebitCardInfo(ClaimsPrincipal currentUser, string username, BankSystemContext _context);
        public Task<ActionResult> CreateDebitCard(ClaimsPrincipal currentUser, string username, BankAccounts bankAccount, BankSystemContext _context, Cards card);
        public Task<ActionResult<Users>> DeleteDebitCard(ClaimsPrincipal currentUser, string username, BankSystemContext _context);
    }
}
