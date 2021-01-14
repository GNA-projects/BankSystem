﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;
using VitoshaBank.Services.DebitCardService.Interfaces;

namespace VitoshaBank.Controllers
{
    [Route("api/card")]
    [ApiController]
    public class DebitCardController : ControllerBase
    {
        private readonly BankSystemContext _context;
        private readonly ILogger<Cards> _logger;
        private readonly IConfiguration _config;
        private readonly IDebitCardService _debitCardService;
       

        public DebitCardController(BankSystemContext context, ILogger<Cards> logger, IConfiguration config, IDebitCardService debitCardService)
        {
            _context = context;
            _logger = logger;
            _config = config;
            _debitCardService = debitCardService;
            
        }
        [HttpDelete("delete/{username}")]
        [Authorize]
        public async Task<ActionResult<Users>> DeleteDebitCard(string username)
        {
            var currentUser = HttpContext.User;
            return await _debitCardService.DeleteDebitCard(currentUser, username, _context);
        }
    }
}
