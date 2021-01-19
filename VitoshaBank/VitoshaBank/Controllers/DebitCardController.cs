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
using VitoshaBank.Services.DebitCardService.Interfaces;

namespace VitoshaBank.Controllers
{
    [Route("api/card")]
    [ApiController]
    public class DebitCardController : ControllerBase
    {
        private readonly BankSystemContext _context;
        private readonly ILogger<Cards> _logger;
        private readonly IDebitCardService _debitCardService;
        private readonly MessageModel _messageModel;
       

        public DebitCardController(BankSystemContext context, ILogger<Cards> logger, IDebitCardService debitCardService)
        {
            _context = context;
            _logger = logger;
            _debitCardService = debitCardService;
            _messageModel = new MessageModel();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<DebitCardResponseModel>> GetDebitCardInfo()
        {
            var currentUser = HttpContext.User;
            string username = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Username").Value;
            return await _debitCardService.GetDebitCardInfo(currentUser, username, _context, _messageModel);
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<ActionResult<Users>> DeleteDebitCard(DebitCardRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _debitCardService.DeleteDebitCard(currentUser, requestModel.Username, _context, _messageModel);
        }
    }
}
