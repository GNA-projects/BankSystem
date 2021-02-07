using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VitoshaBank.Data.MessageModels;
using VitoshaBank.Data.Models;
using VitoshaBank.Services.CalculateDividendService;
using VitoshaBank.Services.DepositPayentService.Interfaces;

namespace VitoshaBank.Services.DepositPayentService
{
    public class DividentPayment : ControllerBase,  IDividentPaymentService
    {
        public async Task<ActionResult<MessageModel>> GetDividentPayment(Deposits deposit, MessageModel messageModel, BankSystemContext _context)
        {
            if (DateTime.Now >= deposit.PaymentDate)
            {
                var dividentAmount = CalculateDivident.GetDividentAmount(deposit.Amount, deposit.Divident,deposit.TermOfPayment);
                deposit.Amount = deposit.Amount + dividentAmount;
                deposit.PaymentDate.AddMonths(deposit.TermOfPayment);
                await _context.SaveChangesAsync();
                messageModel.Message = "Deposit divident applied successfully!";
                return StatusCode(200, messageModel);
            }
            return null;
        }
    }
}
