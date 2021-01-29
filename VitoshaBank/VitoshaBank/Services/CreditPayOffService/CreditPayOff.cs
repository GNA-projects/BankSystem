using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VitoshaBank.Data.MessageModels;
using VitoshaBank.Data.Models;

namespace VitoshaBank.Services.CreditPayOffService
{
    public class CreditPayOff : ControllerBase
    {
        public async Task<ActionResult<MessageModel>> GetCreditPayOff(Credits credit, MessageModel messageModel, BankSystemContext _context)
        {
            if (DateTime.Now == credit.PaymentDate)
            {
                if (credit.Instalment <= credit.Amount)
                {
                    credit.Amount = credit.Amount - credit.Instalment;
                    credit.CreditAmountLeft = credit.CreditAmountLeft - credit.Instalment;
                    credit.PaymentDate = DateTime.Now.AddMonths(1);
                    await _context.SaveChangesAsync();
                    messageModel.Message = "Credit instalment payed off successfully from Credit Account!";
                    return StatusCode(200, messageModel);
                }
                else
                {
                    if (credit.Instalment<=credit.User.BankAccounts.Amount)
                    {
                        credit.User.BankAccounts.Amount = credit.User.BankAccounts.Amount - credit.Instalment;
                        credit.CreditAmountLeft = credit.CreditAmountLeft - credit.Instalment;
                        credit.PaymentDate = DateTime.Now.AddMonths(1);
                        await _context.SaveChangesAsync();
                        messageModel.Message = "Credit instalment payed off successfully from Bank Account!";
                        return StatusCode(200, messageModel);
                    }
                    else
                    {
                        messageModel.Message = "You don't have enough money to pay off Your instalment! Come to our office as soon as possible to discuss what happens from now on!";
                        return StatusCode(406, messageModel);
                    }
                }
            }
            else
            {
                return null;
            }
        }
    }
}
