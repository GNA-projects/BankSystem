using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;
using VitoshaBank.Data.ResponseModels;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;

namespace VitoshaBank.Services.TransactionService
{
    public class TransactionService : ControllerBase
    {
        public async Task<ActionResult> CreateTransaction(ClaimsPrincipal currentUser, string username, BankAccounts sender, BankAccounts reciever, Transactions transaction, BankSystemContext _context)
        {
            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                var bankSender = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Iban == sender.Iban);
                var bankReciever = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Iban == reciever.Iban);

                if (bankSender != null && bankReciever != null)
                {

                    if (ValidateTransaction(transaction, bankSender.Amount))
                    {
                        transaction.SenderAccountId = bankSender.Id;
                        transaction.TransactionAmount = 10;
                        transaction.RecieverAccountId = bankReciever.Id;
                        transaction.Date = DateTime.Now;
                        _context.Add(transaction);
                        bankSender.Amount = bankSender.Amount - transaction.TransactionAmount;
                        bankReciever.Amount = bankReciever.Amount + transaction.TransactionAmount;
                        await _context.SaveChangesAsync();

                        return Ok();
                    }

                    else
                    {
                        return BadRequest("Idiot don't put negative value!");
                    }

                }
                else
                    return BadRequest("Incorrect IBAN");
            }
            else
                return Unauthorized();
        }


        public async Task<ActionResult<TransactionResponseModel>> GetTransactionInfo(ClaimsPrincipal currentUser, string username, BankSystemContext _context)
        {
            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
                Transactions transactionExists = null;
                //transactionExists.SenderAccount.User != userAuthenticate || transactionExists.RecieverAccount.User != userAuthenticate)

                if (userAuthenticate == null)
                {
                    return NotFound();
                }
                else
                {
                    var senderTransactions = userAuthenticate.BankAccounts.TransactionsSenderAccount;
                    var recieverTransactions = userAuthenticate.BankAccounts.TransactionsRecieverAccount;
                    var allTransactions = new List<TransactionResponseModel>();
                    if (senderTransactions.Count != 0)
                    {
                        foreach (var transaction in senderTransactions)
                        {
                            TransactionResponseModel transactionsResponseModel = new TransactionResponseModel();
                            transactionsResponseModel.Amount = transaction.TransactionAmount;
                            transactionsResponseModel.date = transaction.Date;
                            transactionsResponseModel.senderIBAN = transaction.SenderAccount.Iban;
                            transactionsResponseModel.reciverIBAN = transaction.RecieverAccount.Iban;
                            allTransactions.Add(transactionsResponseModel);
                        }
                        return Ok(allTransactions);
                    }
                    return Ok("You don't have any transactions!");
                }
            }

            return Unauthorized();
        }

        private bool ValidateTransaction(Transactions transactions, decimal Amount)
        {
            if (transactions.TransactionAmount > Amount)
            {
                return false;
            }
            return true;
        }
    }
}

