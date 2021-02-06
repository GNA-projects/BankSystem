using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VitoshaBank.Data.MessageModels;
using VitoshaBank.Data.Models;
using VitoshaBank.Services.CreditPayOffService.Interfaces;

namespace VitoshaBank.Services.CreditPayOffService
{
    public class CreditPayOff : ControllerBase, ICreditPayOffService
    {
        public async Task<ActionResult<MessageModel>> GetCreditPayOff(Credits credit, MessageModel messageModel, BankSystemContext _context)
        {
            while (DateTime.Now >= credit.PaymentDate)
            {
                if (credit.Instalment <= credit.Amount)
                {
                    credit.Amount = credit.Amount - credit.Instalment;
                    credit.CreditAmountLeft = credit.CreditAmountLeft - credit.Instalment;
                    credit.PaymentDate = credit.PaymentDate.AddMonths(1);
                    await _context.SaveChangesAsync();
                    messageModel.Message = "Credit instalment payed off successfully from Credit Account!";
                    return StatusCode(200, messageModel);
                }
                else
                {
                    var bankAccount = _context.ChargeAccounts.FirstOrDefault(x => x.UserId == credit.UserId);
                    if (credit.Instalment<= bankAccount.Amount)
                    {
                        bankAccount.Amount = bankAccount.Amount - credit.Instalment;
                        credit.CreditAmountLeft = credit.CreditAmountLeft - credit.Instalment;
                        credit.PaymentDate = credit.PaymentDate.AddMonths(1);
                        await _context.SaveChangesAsync();
                        messageModel.Message = "Credit instalment payed off successfully from Charge Account!";
                        return StatusCode(200, messageModel);
                    }
                    else
                    {
                        messageModel.Message = "You don't have enough money to pay off Your instalment! Come to our office as soon as possible to discuss what happens from now on!";
                        return StatusCode(406, messageModel);
                    }
                }
            }
            return null;
        }
    }
}
