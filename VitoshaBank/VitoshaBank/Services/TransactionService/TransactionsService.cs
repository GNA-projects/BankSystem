using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.MessageModels;
using VitoshaBank.Data.Models;
using VitoshaBank.Data.ResponseModels;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;
using VitoshaBank.Services.TransactionService.Interfaces;

namespace VitoshaBank.Services.TransactionService
{
    public class TransactionsService : ControllerBase, ITransactionService
    {
        public async Task<ActionResult> CreateTransaction(ClaimsPrincipal currentUser, decimal amount, Transactions transaction, string senderType, string recieverType, string reason, BankSystemContext _context, MessageModel _messageModel)
        {
            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                object senderAcc = null;
                object recieverAcc = null;
                TransactionResponseModel sender = new TransactionResponseModel();
                TransactionResponseModel reciever = new TransactionResponseModel();
                if (transaction.SenderAccountInfo.Contains("BG18VITB") && transaction.SenderAccountInfo.Length == 23)
                {
                    sender.IsIBAN = true;
                    sender.senderInfo = transaction.SenderAccountInfo;
                    if (transaction.RecieverAccountInfo.Contains("BG18VITB") && transaction.RecieverAccountInfo.Length == 23)
                    {
                        reciever.IsIBAN = true;
                    }
                    
                }
                else if (transaction.RecieverAccountInfo.Contains("BG18VITB") && transaction.RecieverAccountInfo.Length == 23)
                {
                    reciever.IsIBAN = true;
                    reciever.reciverInfo = transaction.RecieverAccountInfo;
                    sender.senderInfo = transaction.SenderAccountInfo;
                }
                else
                {
                    _messageModel.Message = "Invalid arguments!";
                    return StatusCode(400, _messageModel);
                }
                //bad request
                if (sender.IsIBAN && reciever.IsIBAN)
                {
                    //Bank Transfer
                    if (senderType == "BankAccount")
                    {
                        senderAcc = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Iban == sender.senderInfo);
                        if (recieverType == "BankAccount")
                        {
                            recieverAcc = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Iban == reciever.reciverInfo);
                        }
                        else if (recieverType == "Deposit")
                        {
                            recieverAcc = await _context.Deposits.FirstOrDefaultAsync(x => x.Iban == reciever.reciverInfo);
                        }
                        else if (recieverType == "Wallet")
                        {
                            recieverAcc = await _context.Wallets.FirstOrDefaultAsync(x => x.Iban == reciever.reciverInfo);
                        }
                        else if (recieverType == "Credit")
                        {
                            recieverAcc = await _context.Credits.FirstOrDefaultAsync(x => x.Iban == reciever.reciverInfo);
                        }
                        else
                        {
                            //invalid
                        }
                        reciever.reciverInfo = transaction.RecieverAccountInfo;
                        transaction.Reason = reason;
                        transaction.SenderAccountInfo = sender.senderInfo;
                        transaction.RecieverAccountInfo = reciever.reciverInfo;
                        transaction.Date = DateTime.Now;
                        transaction.TransactionAmount = amount;
                        _context.Add(transaction);
                        await _context.SaveChangesAsync();
                        _messageModel.Message = "Money send successfully!";
                        return StatusCode(200, _messageModel);
                    }
                    else if (senderType == "Deposit")
                    {
                        senderAcc = await _context.Deposits.FirstOrDefaultAsync(x => x.Iban == sender.senderInfo);
                        if (recieverType == "BankAccount")
                        {
                            recieverAcc = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Iban == reciever.reciverInfo);
                        }
                        else
                        {
                            _messageModel.Message = "Invalid arguments!";
                            return StatusCode(400, _messageModel);
                        }
                    }
                    else
                    {
                        _messageModel.Message = "Invalid arguments!";
                        return StatusCode(400, _messageModel);
                    }

                    reciever.reciverInfo = transaction.RecieverAccountInfo;
                    transaction.Reason = reason;
                    transaction.SenderAccountInfo = sender.senderInfo;
                    transaction.RecieverAccountInfo = reciever.reciverInfo;
                    transaction.Date = DateTime.Now;
                    transaction.TransactionAmount = amount;
                    _context.Add(transaction);
                    await _context.SaveChangesAsync();
                    _messageModel.Message = "Money send successfully!";
                    return StatusCode(200, _messageModel);
                }
                else if (sender.IsIBAN && !reciever.IsIBAN)
                {
                    //Purchase
                    if (senderType == "BankAccount")
                    {
                        senderAcc = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Iban == sender.senderInfo);
                    }
                    else if (senderType == "Wallet")
                    {
                        senderAcc = await _context.Wallets.FirstOrDefaultAsync(x => x.Iban == sender.senderInfo);
                    }
                    else if (senderType == "Credit")
                    {
                        senderAcc = await _context.Credits.FirstOrDefaultAsync(x => x.Iban == sender.senderInfo);
                    }
                    else
                    {
                        _messageModel.Message = "Invalid arguments!";
                        return StatusCode(400, _messageModel);
                    }
                    reciever.reciverInfo = transaction.RecieverAccountInfo;
                    transaction.Reason = reason;
                    transaction.SenderAccountInfo = sender.senderInfo;
                    transaction.RecieverAccountInfo = reciever.reciverInfo;
                    transaction.Date = DateTime.Now;
                    transaction.TransactionAmount = amount;
                    _context.Add(transaction);
                    await _context.SaveChangesAsync();
                    _messageModel.Message = "Purchase successfull!";
                    return StatusCode(200, _messageModel);
                }
                else if (!sender.IsIBAN && reciever.IsIBAN)
                {
                    //zaplata
                    if (recieverType == "BankAccount")
                    {
                        recieverAcc = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Iban == reciever.reciverInfo);
                    }
                    else
                    {
                        _messageModel.Message = "Invalid arguments!";
                        return StatusCode(400, _messageModel);
                    }
                    reciever.reciverInfo = transaction.RecieverAccountInfo;
                    transaction.Reason = reason;
                    transaction.SenderAccountInfo = sender.senderInfo;
                    transaction.RecieverAccountInfo = reciever.reciverInfo;
                    transaction.Date = DateTime.Now;
                    transaction.TransactionAmount = amount;
                    _context.Add(transaction);
                    await _context.SaveChangesAsync();
                    _messageModel.Message = "Money recieved successfully!";
                    return StatusCode(200, _messageModel);
                }
            }
            _messageModel.Message = "You are not autorized to do such actions!";
            return StatusCode(403, _messageModel);

        }
        //    public async Task<ActionResult<TransactionResponseModel>> GetTransactionInfo(ClaimsPrincipal currentUser, string username, BankSystemContext _context)
        //    {
        //        if (currentUser.HasClaim(c => c.Type == "Roles"))
        //        {
        //            var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        //            List<TransactionResponseModel> allTransactions = null;


        //            if (userAuthenticate == null)
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                //var senderTransactions = userAuthenticate.BankAccounts.TransactionsSenderAccount;
        //                //var recieverTransactions = userAuthenticate.BankAccounts.TransactionsRecieverAccount;
        //                //allTransactions = new List<TransactionResponseModel>();
        //                //if (senderTransactions.Count != 0)
        //                //{
        //                //    foreach (var transaction in senderTransactions)
        //                //    {
        //                //        TransactionResponseModel transactionsResponseModel = new TransactionResponseModel();
        //                //        transactionsResponseModel.Amount = transaction.TransactionAmount;
        //                //        transactionsResponseModel.Date = transaction.Date;
        //                //        transactionsResponseModel.senderIBAN = transaction.SenderAccount.Iban;
        //                //        transactionsResponseModel.reciverIBAN = transaction.RecieverAccount.Iban;
        //                //        allTransactions.Add(transactionsResponseModel);
        //                //    }
        //                return Ok(allTransactions);
        //            }

        //            return Ok("You don't have any transactions!");
        //        }
        //    }

        //        return Unauthorized();
        //}

        //private bool ValidateTransaction(Transactions transactions, decimal Amount)
        //{
        //    if (transactions.TransactionAmount > Amount)
        //    {
        //        return false;
        //    }

        //    return true;
        //}
    }
}

