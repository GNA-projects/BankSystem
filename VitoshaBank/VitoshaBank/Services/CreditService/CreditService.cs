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
using VitoshaBank.Services.CalculateInterestService;
using VitoshaBank.Services.CreditPayOffService;
using VitoshaBank.Services.CreditPayOffService.Interfaces;
using VitoshaBank.Services.CreditService.Interfaces;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;
using VitoshaBank.Services.TransactionService.Interfaces;

namespace VitoshaBank.Services.CreditService
{
    public class CreditService : ControllerBase, ICreditService
    {
        public async Task<ActionResult<CreditResponseModel>> GetCreditInfo(ClaimsPrincipal currentUser, string username, ICreditPayOffService _payOffService, BankSystemContext _context, MessageModel _messageModel)
        {
            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
                Credits creditsExists = null;
                CreditResponseModel creditResponseModel = new CreditResponseModel();

                if (userAuthenticate == null)
                {
                    _messageModel.Message = "User not found";
                    return StatusCode(404, _messageModel);
                }
                else
                {
                    creditsExists = await _context.Credits.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                }

                if (creditsExists != null)
                {
                    creditResponseModel.IBAN = creditsExists.Iban;
                    creditResponseModel.Amount = Math.Round(creditsExists.Amount);
                    creditResponseModel.CreditAmount = Math.Round(creditsExists.CreditAmount, 2);
                    creditResponseModel.Instalment = Math.Round(creditsExists.Instalment, 2);
                    await _payOffService.GetCreditPayOff(creditsExists, _messageModel, _context);
                    return StatusCode(200, creditResponseModel);
                }
                _messageModel.Message = "You don't have a Credit";
                return StatusCode(400, _messageModel);
            }

            _messageModel.Message = "You are not autorized to do such actions!";
            return StatusCode(403, _messageModel);
        }
        public async Task<ActionResult<CreditResponseModel>> GetPayOffInfo(ClaimsPrincipal currentUser, string username, ICreditPayOffService _payOffService, BankSystemContext _context, MessageModel _messageModel)
        {
            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
                Credits creditsExists = null;
                CreditResponseModel creditResponseModel = new CreditResponseModel();

                if (userAuthenticate == null)
                {
                    _messageModel.Message = "User not found";
                    return StatusCode(404, _messageModel);
                }
                else
                {
                    creditsExists = await _context.Credits.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                }

                if (creditsExists != null)
                {

                    await _payOffService.GetCreditPayOff(creditsExists, _messageModel, _context);
                    if (creditsExists.CreditAmountLeft == 0 && creditsExists.CreditAmount > 0)
                    {
                        _messageModel.Message = "You have payed your credit!";
                        await this.DeleteCredit(currentUser, username, _context, _messageModel);
                    }
                    else
                        _messageModel.Message = "Successfully payed montly pay off!";
                    return StatusCode(200, _messageModel);
                }
                _messageModel.Message = "You don't have a Credit";
                return StatusCode(400, _messageModel);
            }

            _messageModel.Message = "You are not autorized to do such actions!";
            return StatusCode(403, _messageModel);
        }
        public async Task<ActionResult<MessageModel>> CreateCredit(ClaimsPrincipal currentUser, string username, Credits credits, int period, IIBANGeneratorService _IBAN, BankSystemContext _context, MessageModel _messageModel)

        {
            string role = "";

            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                string userRole = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Roles").Value;
                role = userRole;
            }

            if (role == "Admin")
            {
                var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
                Credits creditsExists = null;

                if (userAuthenticate != null)
                {
                    creditsExists = await _context.Credits.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                }


                if (creditsExists == null)
                {
                    if (ValidateUser(userAuthenticate) && ValidateCredit(credits))
                    {
                        credits.UserId = userAuthenticate.Id;
                        credits.Iban = _IBAN.GenerateIBANInVitoshaBank("Credit", _context);
                        credits.Interest = 6.9m;
                        credits.PaymentDate = DateTime.Now.AddMonths(1);
                        credits.CreditAmount = CalculateInterest.CalculateCreditAmount(credits.Amount, period, credits.Interest);
                        credits.Instalment = CalculateInterest.CalculateInstalment(credits.CreditAmount, credits.Interest, 5);
                        credits.CreditAmountLeft = credits.CreditAmount;
                        _context.Add(credits);
                        await _context.SaveChangesAsync();
                        _messageModel.Message = "Credit created successfully!";
                        return StatusCode(200, _messageModel);
                    }
                    else if (ValidateUser(userAuthenticate) == false)
                    {
                        _messageModel.Message = "User not found!";
                        return StatusCode(404, _messageModel);
                    }
                    else if (ValidateCredit(credits) == false)
                    {
                        _messageModel.Message = "Invalid parameteres!";
                        return StatusCode(400, _messageModel);
                    }
                }
                _messageModel.Message = "User already has a Credit!";
                return StatusCode(400, _messageModel);
            }
            else
            {
                _messageModel.Message = "You are not autorized to do such actions!";
                return StatusCode(403, _messageModel);
            }
        }
        public async Task<ActionResult<MessageModel>> SimulatePurchase(Credits credit, string product, string reciever, ClaimsPrincipal currentUser, string username, decimal amount, BankSystemContext _context, ITransactionService _transactionService, MessageModel _messageModel)
        {
            var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            Credits creditExists = null;

            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                if (userAuthenticate != null)
                {
                    creditExists = await _context.Credits.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id && x.Iban == credit.Iban);
                }
                else
                {
                    _messageModel.Message = "User not found!";
                    return StatusCode(404, _messageModel);
                }

                if (creditExists != null)
                {
                    if (ValidateCreditAmount(amount, creditExists) && ValidateCredit(creditExists))
                    {
                        creditExists.Amount = creditExists.Amount - amount;
                        Transactions transaction = new Transactions();
                        transaction.SenderAccountInfo = creditExists.Iban;
                        transaction.RecieverAccountInfo = reciever;
                        await _transactionService.CreateTransaction(userAuthenticate, currentUser, amount, transaction, $"Purchasing {product}", _context, _messageModel);
                        await _context.SaveChangesAsync();
                        _messageModel.Message = $"Succesfully purhcased {product}.";
                        return StatusCode(200, _messageModel);
                    }
                    else if (ValidateCreditAmount(amount, credit) == false)
                    {
                        _messageModel.Message = "Invalid payment amount!";
                        return StatusCode(400, _messageModel);
                    }
                    else if (ValidateCredit(creditExists) == false)
                    {
                        _messageModel.Message = "You don't have enough money in Bank account!";
                        return StatusCode(406, _messageModel);
                    }

                }
                else
                {
                    _messageModel.Message = "Credit not found";
                    return StatusCode(404, _messageModel);
                }
            }
            _messageModel.Message = "You are not autorized to do such actions!";
            return StatusCode(403, _messageModel);

        }
        public async Task<ActionResult<MessageModel>> AddMoney(Credits credit, ClaimsPrincipal currentUser, string username, decimal amount, BankSystemContext _context, ITransactionService _transaction, MessageModel _messageModel)
        {

            var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            Credits creditsExists = null;
            ChargeAccounts bankAccounts = null;

            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                if (userAuthenticate != null)
                {
                    creditsExists = await _context.Credits.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id && x.Iban == credit.Iban);
                }
                else
                {
                    _messageModel.Message = "User not found!";
                    return StatusCode(404, _messageModel);
                }

                if (creditsExists != null)
                {
                    bankAccounts = _context.ChargeAccounts.FirstOrDefault(x => x.UserId == userAuthenticate.Id);
                    return await ValidateDepositAmountAndCredit(userAuthenticate, creditsExists, currentUser, amount, bankAccounts, _context, _transaction, _messageModel);
                }
                else
                {
                    _messageModel.Message = "Credit not found";
                    return StatusCode(404, _messageModel);
                }
            }

            _messageModel.Message = "You are not autorized to do such actions!";
            return StatusCode(403, _messageModel);
        }
        public async Task<ActionResult<MessageModel>> Withdraw(Credits credit, ClaimsPrincipal currentUser, string username, decimal amount, string reciever, BankSystemContext _context, ITransactionService _transaction, MessageModel _messageModel)
        {
            var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            Credits creditExists = null;

            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                if (userAuthenticate != null)
                {
                    creditExists = await _context.Credits.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                }
                else
                {
                    _messageModel.Message = "User not found!";
                    return StatusCode(404, _messageModel);
                }

                if (creditExists != null)
                {
                    if (ValidateDepositAmountBankAccount(amount) && ValidateCredit(creditExists) && ValidateMinAmount(creditExists, amount))
                    {
                        creditExists.Amount = creditExists.Amount - amount;
                        Transactions transactions = new Transactions();
                        transactions.SenderAccountInfo = credit.Iban;
                        transactions.RecieverAccountInfo = reciever;
                        await _transaction.CreateTransaction(userAuthenticate, currentUser, amount, transactions, $"Withdrawing {amount} leva", _context, _messageModel);
                        await _context.SaveChangesAsync();
                        _messageModel.Message = $"Succesfully withdrawed {amount} leva.";
                        return StatusCode(200, _messageModel);
                    }
                    else if (ValidateDepositAmountBankAccount(amount) == false)
                    {
                        _messageModel.Message = "Invalid payment amount!";
                        return StatusCode(400, _messageModel);
                    }
                    else if (ValidateCredit(creditExists) == false)
                    {
                        _messageModel.Message = "You don't have enough money in Credit Account!";
                        return StatusCode(406, _messageModel);
                    }
                    else if (ValidateMinAmount(creditExists, amount) == false)
                    {
                        _messageModel.Message = "Min amount is 10 lv!";
                        return StatusCode(406, _messageModel);
                    }

                }
                else
                {
                    _messageModel.Message = "Credit not found";
                    return StatusCode(404, _messageModel);
                }
            }
            _messageModel.Message = "You are not autorized to do such actions!";
            return StatusCode(403, _messageModel);
        }
        public async Task<ActionResult<MessageModel>> DeleteCredit(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel _messageModel)
        {
            string role = "";

            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                string userRole = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Roles").Value;
                role = userRole;
            }

            if (role == "Admin")
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
                Credits creditsExists = null;

                if (user != null)
                {
                    creditsExists = await _context.Credits.FirstOrDefaultAsync(x => x.UserId == user.Id);
                }

                if (user == null)
                {
                    _messageModel.Message = "User not found!";
                    return StatusCode(404, _messageModel);
                }
                else if (creditsExists == null)
                {
                    _messageModel.Message = "User deosn't have a credit!";
                    return StatusCode(400, _messageModel);
                }

                _context.Credits.Remove(creditsExists);
                await _context.SaveChangesAsync();

                _messageModel.Message = "Credit deleted successfully!";
                return StatusCode(200, _messageModel);
            }
            else
            {
                _messageModel.Message = "You are not autorized to do such actions!";
                return StatusCode(403, _messageModel);
            }
        }
        private bool ValidateMinAmount(Credits credit, decimal amount)
        {
            if (amount <= credit.Amount)
            {
                return true;
            }
            return false;
        }
        private bool ValidateDepositAmountBankAccount(decimal amount)
        {
            if (amount >= 10)
            {
                return true;
            }
            return false;
        }
        private bool ValidateCreditAmount(decimal amount, Credits credit)
        {
            if (credit.Amount < amount)
            {
                return false;
            }
            return true;
        }
        private bool ValidateCredit(Credits credits)
        {
            if (credits.Amount < 0)
            {
                return false;
            }
            return true;
        }
        private bool ValidateUser(Users user)
        {
            if (user != null)
            {
                return true;
            }
            return false;
        }
        private async Task<ActionResult> ValidateDepositAmountAndCredit(Users userAuthenticate, Credits creditExists, ClaimsPrincipal currentUser, decimal amount, ChargeAccounts bankAccount, BankSystemContext _context, ITransactionService _transaction, MessageModel _messageModel)
        {
            if (amount < 0)
            {
                _messageModel.Message = "Invalid payment amount!";
                return StatusCode(400, _messageModel);
            }
            else if (amount == 0)
            {
                _messageModel.Message = "Put amount more than 0.00lv";
                return StatusCode(400, _messageModel);
            }
            else
            {
                if (bankAccount != null && bankAccount.Amount > amount)
                {
                    creditExists.Amount = creditExists.Amount + amount;
                    bankAccount.Amount = bankAccount.Amount - amount;
                    Transactions transaction = new Transactions();
                    transaction.SenderAccountInfo = bankAccount.Iban;
                    transaction.RecieverAccountInfo = creditExists.Iban;
                    await _transaction.CreateTransaction(userAuthenticate, currentUser, amount, transaction, $"Depositing money in Credit Account", _context, _messageModel);
                    await _context.SaveChangesAsync();
                }
                else if (bankAccount.Amount < amount)
                {
                    _messageModel.Message = "You don't have enough money in bank account!";
                    return StatusCode(406, _messageModel);
                }
                else if (bankAccount == null)
                {
                    _messageModel.Message = "You don't have a bank account";
                    return StatusCode(400, _messageModel);
                }
            }
            _messageModel.Message = $"Succesfully deposited {amount} leva.";
            return StatusCode(200, _messageModel);
        }
    }
}
