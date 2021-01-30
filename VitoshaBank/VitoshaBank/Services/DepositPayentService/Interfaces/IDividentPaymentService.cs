using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VitoshaBank.Data.MessageModels;
using VitoshaBank.Data.Models;

namespace VitoshaBank.Services.DepositPayentService.Interfaces
{
    public interface IDividentPaymentService
    {
        public Task<ActionResult<MessageModel>> GetDividentPayment(Deposits deposit, MessageModel messageModel, BankSystemContext _context);
    }
}
