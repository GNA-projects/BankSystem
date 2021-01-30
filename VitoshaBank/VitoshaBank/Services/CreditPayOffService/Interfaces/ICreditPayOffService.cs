using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VitoshaBank.Data.MessageModels;
using VitoshaBank.Data.Models;

namespace VitoshaBank.Services.CreditPayOffService.Interfaces
{
    public interface ICreditPayOffService
    {
        public Task<ActionResult<MessageModel>> GetCreditPayOff(Credits credit, MessageModel messageModel, BankSystemContext _context);
    }
}
