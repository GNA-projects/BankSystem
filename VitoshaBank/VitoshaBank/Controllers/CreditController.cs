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
using VitoshaBank.Services.CreditService;
using VitoshaBank.Services.CreditService.Interfaces;
using VitoshaBank.Services.DebitCardService.Interfaces;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;

namespace VitoshaBank.Controllers
{
    [Route("api/credit")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly BankSystemContext _context;
        private readonly ILogger<Credits> _logger;
        private readonly IConfiguration _config;
        private readonly ICreditService _creditService;
        private readonly IIBANGeneratorService _IBAN;
        private readonly ICalculateInterestService _interestService; 

        public CreditController(BankSystemContext context, ILogger<Credits> logger, IConfiguration config, ICreditService creditService, IIBANGeneratorService IBAN)
        {
            _context = context;
            _logger = logger;
            _config = config;
            _creditService = creditService;
            _IBAN = IBAN;
        }
        [HttpPost("create/{username}")]
        [Authorize]
        public async Task<ActionResult> CreateCredit(Credits credits, string username)
        {
            var currentUser = HttpContext.User;
            return await _creditService.CreateCredit(currentUser, username, credits, _IBAN, _context, _interestService);
        }

        [HttpDelete("delete/{username}")]
        [Authorize]
        public async Task<ActionResult<Users>> DeleteCredit(string username)
        {
            var currentUser = HttpContext.User;
            return await _creditService.DeleteCredit(currentUser, username, _context);
        }
        [HttpGet("get/{username}")]
        [Authorize]
        public async Task<ActionResult<CreditResponseModel>> GetWalletInfo(string username)
        {
            var currentUser = HttpContext.User;
            return await _creditService.GetCreditInfo(currentUser, username, _context);
        }
    }

}
