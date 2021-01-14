using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;
using VitoshaBank.Data.ResponseModels;
using VitoshaBank.Services.BankAccountService.Interfaces;
using VitoshaBank.Services.DebitCardService.Interfaces;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;

namespace VitoshaBank.Controllers
{
    [Route("api/bankaccount")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly BankSystemContext _context;
        private readonly ILogger<Users> _logger;
        private readonly IConfiguration _config;
        private readonly IBankAccountService _bankAccountService;
        private readonly IDebitCardService _debitCardService;
        private readonly IIBANGeneratorService _IBAN;

        public BankAccountController(BankSystemContext context, ILogger<Users> logger, IConfiguration config, IBankAccountService bankAccountService, IIBANGeneratorService IBAN, IDebitCardService debitCardService)
        {
            _context = context;
            _logger = logger;
            _config = config;
            _bankAccountService = bankAccountService;
            _debitCardService = debitCardService;
            _IBAN = IBAN;
        }

        [HttpGet("get/{username}")]
        [Authorize]
        public async Task<ActionResult<BankAccountResponseModel>> GetBankAccountInfo(string username)
        {
            var currentUser = HttpContext.User;
            return await _bankAccountService.GetBankAccountInfo(currentUser, username, _context);
        }

        [HttpPost("create/{username}")]
        [Authorize]
        public async Task<ActionResult> CreateBankAccount(BankAccounts bankAccount, string username)
        {
            var currentUser = HttpContext.User;
            return await _bankAccountService.CreateBankAccount(currentUser, username, bankAccount,_IBAN, _context, _debitCardService);
        }

        [HttpDelete("delete/{username}")]
        [Authorize]
        public async Task<ActionResult<Users>> DeleteBankAccount (string username)
        {
            var currentUser = HttpContext.User;
            return await _bankAccountService.DeleteBankAccount(currentUser, username, _context);
        }
    }
}
