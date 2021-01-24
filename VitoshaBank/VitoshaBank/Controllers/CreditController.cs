using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VitoshaBank.Data.MessageModels;
using VitoshaBank.Data.Models;
using VitoshaBank.Data.RequestModels;
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
        private readonly ICreditService _creditService;
        private readonly IIBANGeneratorService _IBAN;
        private readonly MessageModel _messageModel;
        

        public CreditController(BankSystemContext context, ILogger<Credits> logger, ICreditService creditService, IIBANGeneratorService IBAN)
        {
            _context = context;
            _logger = logger;
            _creditService = creditService;
            _IBAN = IBAN;
            _messageModel = new MessageModel();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<CreditResponseModel>> GetCreditInfo()
        {
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _creditService.GetCreditInfo(currentUser, username, _context,  _messageModel);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> CreateCredit(CreditRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _creditService.CreateCredit(currentUser, requestModel.Username, requestModel.Credit, requestModel.Period, _IBAN, _context,  _messageModel);
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> DeleteCredit(CreditRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _creditService.DeleteCredit(currentUser, requestModel.Username, _context,  _messageModel);
        }

        [HttpPut("purchase")]
        [Authorize]

        public async Task<ActionResult<MessageModel>> Purchase(CreditRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _creditService.SimulatePurchase(requestModel.Credit, requestModel.Product, currentUser, requestModel.Username, requestModel.Amount, _context, _messageModel);
        }

        [HttpPut("deposit")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> Deposit(CreditRequestModel requestModel)
        {
            //amount = 0.50M;
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _creditService.DepositMoney(requestModel.Credit, currentUser, username, requestModel.Amount, _context, _messageModel);
        }

    }

}
