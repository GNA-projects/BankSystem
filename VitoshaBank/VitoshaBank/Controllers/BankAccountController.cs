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
using VitoshaBank.Data.RequestModels;
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
        private readonly ILogger<BankAccounts> _logger;
        private readonly IBankAccountService _bankAccountService;
        private readonly IDebitCardService _debitCardService;
        private readonly IIBANGeneratorService _IBAN;

        public BankAccountController(BankSystemContext context, ILogger<BankAccounts> logger, IBankAccountService bankAccountService, IIBANGeneratorService IBAN, IDebitCardService debitCardService)
        {
            _context = context;
            _logger = logger;
            _bankAccountService = bankAccountService;
            _debitCardService = debitCardService;
            _IBAN = IBAN;
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<ActionResult<BankAccountResponseModel>> GetBankAccountInfo()
        {
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _bankAccountService.GetBankAccountInfo(currentUser, username, _context);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult> CreateBankAccount(BankAccountRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _bankAccountService.CreateBankAccount(currentUser, requestModel.Username, requestModel.BankAccount,_IBAN, _context, _debitCardService);
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<ActionResult<Users>> DeleteBankAccount(UserResponseModel username)
        {
            var currentUser = HttpContext.User;
            return await _bankAccountService.DeleteBankAccount(currentUser, username.Username, _context);
        }
    }
}
