﻿using Microsoft.AspNetCore.Mvc;
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
using VitoshaBank.Services.CreditService.Interfaces;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;

namespace VitoshaBank.Services.CreditService
{
    public class CreditService : ControllerBase, ICreditService
    {
        public async Task<ActionResult<CreditResponseModel>> GetCreditInfo(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel _messageModel)
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
                    creditResponseModel.Amount = creditsExists.Amount;
                    creditResponseModel.CreditAmount = creditsExists.CreditAmount;
                    creditResponseModel.Instalment = creditsExists.Instalment;

                    return StatusCode(200, creditResponseModel);
                }
                _messageModel.Message = "You don't have a credit";
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
                _messageModel.Message = "User already has a credit!";
                return StatusCode(400, _messageModel);
            }
            else
            {
                _messageModel.Message = "You are not autorized to do such actions!";
                return StatusCode(403, _messageModel);
            }
        }
        public async Task<ActionResult<MessageModel>> SimulatePurchase(Credits credit, string product, ClaimsPrincipal currentUser, string username, decimal amount, BankSystemContext _context, MessageModel _messageModel)
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
                    if (ValidateCreditAmount(amount, credit) && ValidateCredit(creditExists))
                    {
                        credit.Amount = credit.Amount - amount;
                        //Transaction transaction = new transactiom"
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
                        _messageModel.Message = "You don't have enough money in bank account!";
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

        public async Task<ActionResult<MessageModel>> DepositMoney(Credits credit, ClaimsPrincipal currentUser, string username, decimal amount, BankSystemContext _context, MessageModel _messageModel)
        {
            //Transactions = new transactions();
            var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            Credits creditsExists = null;
            BankAccounts bankAccounts = null;

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
                    bankAccounts = _context.BankAccounts.FirstOrDefault(x => x.UserId == userAuthenticate.Id);
                    return await ValidateDepositAmountAndBankAccount(creditsExists, amount, bankAccounts, _context, _messageModel);
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
                return StatusCode(200);
            }
            else
            {
                _messageModel.Message = "You are not autorized to do such actions!";
                return StatusCode(403, _messageModel);
            }
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
        private async Task<ActionResult> ValidateDepositAmountAndBankAccount(Credits creditExists, decimal amount, BankAccounts bankAccount, BankSystemContext _context, MessageModel _messageModel)
        {
            if (amount < 0)
            {
                _messageModel.Message = "Don't put negative amount!";
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
