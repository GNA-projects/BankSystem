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
                    sender.SenderInfo = transaction.SenderAccountInfo;
                    if (transaction.RecieverAccountInfo.Contains("BG18VITB") && transaction.RecieverAccountInfo.Length == 23)
                    {
                        reciever.IsIBAN = true;
                    }

                }
                else if (transaction.RecieverAccountInfo.Contains("BG18VITB") && transaction.RecieverAccountInfo.Length == 23)
                {
                    reciever.IsIBAN = true;
                    reciever.ReciverInfo = transaction.RecieverAccountInfo;
                    sender.SenderInfo = transaction.SenderAccountInfo;
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
                        senderAcc = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Iban == sender.SenderInfo);
                        if (recieverType == "BankAccount")
                        {
                            recieverAcc = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Iban == reciever.ReciverInfo);
                        }
                        else if (recieverType == "Deposit")
                        {
                            recieverAcc = await _context.Deposits.FirstOrDefaultAsync(x => x.Iban == reciever.ReciverInfo);
                        }
                        else if (recieverType == "Wallet")
                        {
                            recieverAcc = await _context.Wallets.FirstOrDefaultAsync(x => x.Iban == reciever.ReciverInfo);
                        }
                        else if (recieverType == "Credit")
                        {
                            recieverAcc = await _context.Credits.FirstOrDefaultAsync(x => x.Iban == reciever.ReciverInfo);
                        }
                        else
                        {
                            //invalid
                        }
                        reciever.ReciverInfo = transaction.RecieverAccountInfo;
                        transaction.Reason = reason;
                        transaction.SenderAccountInfo = sender.SenderInfo;
                        transaction.RecieverAccountInfo = reciever.ReciverInfo;
                        transaction.Date = DateTime.Now;
                        transaction.TransactionAmount = amount;
                        _context.Add(transaction);
                        await _context.SaveChangesAsync();
                        _messageModel.Message = "Money send successfully!";
                        return StatusCode(200, _messageModel);
                    }
                    else if (senderType == "Deposit")
                    {
                        senderAcc = await _context.Deposits.FirstOrDefaultAsync(x => x.Iban == sender.SenderInfo);
                        if (recieverType == "BankAccount")
                        {
                            recieverAcc = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Iban == reciever.ReciverInfo);
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

                    reciever.ReciverInfo = transaction.RecieverAccountInfo;
                    transaction.Reason = reason;
                    transaction.SenderAccountInfo = sender.SenderInfo;
                    transaction.RecieverAccountInfo = reciever.ReciverInfo;
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
                        senderAcc = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Iban == sender.SenderInfo);
                    }
                    else if (senderType == "Wallet")
                    {
                        senderAcc = await _context.Wallets.FirstOrDefaultAsync(x => x.Iban == sender.SenderInfo);
                    }
                    else if (senderType == "Credit")
                    {
                        senderAcc = await _context.Credits.FirstOrDefaultAsync(x => x.Iban == sender.SenderInfo);
                    }
                    else
                    {
                        _messageModel.Message = "Invalid arguments!";
                        return StatusCode(400, _messageModel);
                    }
                    reciever.ReciverInfo = transaction.RecieverAccountInfo;
                    transaction.Reason = reason;
                    transaction.SenderAccountInfo = sender.SenderInfo;
                    transaction.RecieverAccountInfo = reciever.ReciverInfo;
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
                        recieverAcc = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Iban == reciever.ReciverInfo);
                    }
                    else
                    {
                        _messageModel.Message = "Invalid arguments!";
                        return StatusCode(400, _messageModel);
                    }
                    reciever.ReciverInfo = transaction.RecieverAccountInfo;
                    transaction.Reason = reason;
                    transaction.SenderAccountInfo = sender.SenderInfo;
                    transaction.RecieverAccountInfo = reciever.ReciverInfo;
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
        public async Task<ActionResult<GetTransactionsResponseModel>> GetTransactionInfo(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel _messageModel)
        {
            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
                List<GetTransactionsResponseModel> allTransactions = new List<GetTransactionsResponseModel>();
                List<Transactions> userTransactions = new List<Transactions>();
                Dictionary<string, string> userDicTransactions = new Dictionary<string, string>();

                BankAccounts userBankAccount = await _context.BankAccounts.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                Deposits userDeposit = await _context.Deposits.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                Credits userCredit = await _context.Credits.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                Wallets userWallets = await _context.Wallets.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);

                if (userAuthenticate == null)
                {
                    _messageModel.Message = "User not found";
                    return StatusCode(404, _messageModel);
                }
                else
                {
                    if (userBankAccount == null)
                    {
                        userBankAccount = new BankAccounts();

                        userBankAccount.Iban = "";
                    }

                    if (userDeposit == null)
                    {
                        userDeposit = new Deposits();
                        userDeposit.Iban = "";
                    }

                    if (userCredit == null)
                    {
                        userCredit = new Credits();
                        userCredit.Iban = "";
                    }

                    if (userWallets == null)
                    {
                        userWallets = new Wallets();
                        userWallets.Iban = "";
                    }

                    userDicTransactions.Add("BankAccount", userBankAccount.Iban);
                    userDicTransactions.Add("Deposit", userDeposit.Iban);
                    userDicTransactions.Add("Credit", userCredit.Iban);
                    userDicTransactions.Add("Wallet", userWallets.Iban);


                    foreach (var IBAN in userDicTransactions)
                    {
                        if (IBAN.Key == "BankAccount" && IBAN.Value != "")
                        {
                            var userSender = await _context.Transactions.Where(x => x.SenderAccountInfo == IBAN.Value).ToListAsync();
                            var userReciver = await _context.Transactions.Where(x => x.RecieverAccountInfo == IBAN.Value).ToListAsync();

                            if (userSender != null)
                            {
                                foreach (var transaction in userSender)
                                {
                                    userTransactions.Add(transaction);
                                }
                            }

                            if (userReciver != null)
                            {
                                foreach (var transaction in userReciver)
                                {
                                    userTransactions.Add(transaction);
                                }
                            }
                        }
                        else if (IBAN.Key == "Deposit" && IBAN.Value != "")
                        {
                            var userSender = await _context.Transactions.Where(x => x.SenderAccountInfo == IBAN.Value).ToListAsync();
                            var userReciver = await _context.Transactions.Where(x => x.RecieverAccountInfo == IBAN.Value).ToListAsync();

                            if (userSender != null)
                            {
                                foreach (var transaction in userSender)
                                {
                                    userTransactions.Add(transaction);
                                }
                            }

                            if (userReciver != null)
                            {
                                foreach (var transaction in userReciver)
                                {
                                    userTransactions.Add(transaction);
                                }
                            }
                        }
                        else if (IBAN.Key == "Credit" && IBAN.Value != "")
                        {
                            var userSender = await _context.Transactions.Where(x => x.SenderAccountInfo == IBAN.Value).ToListAsync();
                            var userReciver = await _context.Transactions.Where(x => x.RecieverAccountInfo == IBAN.Value).ToListAsync();

                            if (userSender != null)
                            {
                                foreach (var transaction in userSender)
                                {
                                    userTransactions.Add(transaction);
                                }
                            }

                            if (userReciver != null)
                            {
                                foreach (var transaction in userReciver)
                                {
                                    userTransactions.Add(transaction);
                                }
                            }
                        }
                        else if (IBAN.Key == "Wallet" && IBAN.Value != "")
                        {
                            var userSender = await _context.Transactions.Where(x => x.SenderAccountInfo == IBAN.Value).ToListAsync();
                            var userReciver = await _context.Transactions.Where(x => x.RecieverAccountInfo == IBAN.Value).ToListAsync();

                            if (userSender != null)
                            {
                                foreach (var transaction in userSender)
                                {
                                    userTransactions.Add(transaction);
                                }
                            }

                            if (userReciver != null)
                            {
                                foreach (var transaction in userReciver)
                                {
                                    userTransactions.Add(transaction);
                                }
                            }
                        }

                    }

                    foreach (var transaction in userTransactions)
                    {
                        GetTransactionsResponseModel responseModel = new GetTransactionsResponseModel();
                        responseModel.SenderInfo = transaction.SenderAccountInfo;
                        responseModel.ReciverInfo = transaction.RecieverAccountInfo;
                        responseModel.Amount = transaction.TransactionAmount;
                        responseModel.Date = transaction.Date;

                        allTransactions.Add(responseModel);
                    }

                }

                if (allTransactions.Count == 0)
                {

                    _messageModel.Message = "User does not have transactions";
                    return StatusCode(404, _messageModel);

                }

                return StatusCode(200, allTransactions);
            }

            _messageModel.Message = "You are not autorized to do such actions!";
            return StatusCode(403, _messageModel);
        }
    }
}

