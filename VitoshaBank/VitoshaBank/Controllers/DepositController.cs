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
using VitoshaBank.Services.CalculateDividendService.Interfaces;
using VitoshaBank.Services.DepositService;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;

namespace VitoshaBank.Controllers
{
    [Route("api/deposit")]
    [ApiController]
    public class DepositController : ControllerBase
    {
        private readonly BankSystemContext _context;
        private readonly ILogger<Deposits> _logger;
        private readonly IDepositService _depositService;
        private readonly IIBANGeneratorService _IBAN;
        private readonly ICalculateDividentService _dividentService;
        private readonly MessageModel _messageModel;
        public DepositController(BankSystemContext context, ILogger<Deposits> logger, IDepositService depositService, IIBANGeneratorService IBAN, ICalculateDividentService dividentDepositService)
        {
            _context = context;
            _logger = logger;
            _depositService = depositService;
            _dividentService = dividentDepositService;
            _IBAN = IBAN;
            _messageModel = new MessageModel();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<DepositResponseModel>> GetDepositInfo()
        {
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _depositService.GetDepositInfo(currentUser, username, _context, _messageModel);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> CreateDeposit(DepositRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _depositService.CreateDeposit(currentUser, requestModel.Username, requestModel.Deposit, _IBAN, _context, _dividentService, _messageModel);
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> DeleteBankAccount(DepositRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _depositService.DeleteDeposit(currentUser, requestModel.Username, _context, _messageModel);
        }
    }
}
