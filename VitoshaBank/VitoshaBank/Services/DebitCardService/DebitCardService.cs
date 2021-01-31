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
using VitoshaBank.Services.BankAccountService.Interfaces;
using VitoshaBank.Services.DebitCardService.Interfaces;
using VitoshaBank.Services.GenerateCardInfoService;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;
using VitoshaBank.Services.Interfaces.UserService;
using VitoshaBank.Services.TransactionService.Interfaces;

namespace VitoshaBank.Services.DebitCardService
{
    public class DebitCardService : ControllerBase, IDebitCardService
    {

        public async Task<ActionResult<DebitCardResponseModel>> GetDebitCardInfo(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel _messageModel)
        {
            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
                BankAccounts bankAccountExits = null;
                Cards debitCardExists = null;
                DebitCardResponseModel debitCardResponseModel = new DebitCardResponseModel();

                if (userAuthenticate == null)
                {
                    _messageModel.Message = "User not found!";
                    return StatusCode(404, _messageModel);
                }
                else
                {
                    bankAccountExits = await _context.BankAccounts.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                    debitCardExists = await _context.Cards.FirstOrDefaultAsync(x => x.BankAccountId == bankAccountExits.Id);
                }

                if (debitCardExists != null)
                {
                    debitCardResponseModel.CardNumber = debitCardExists.CardNumber;
                    if (debitCardResponseModel.CardNumber.StartsWith('5'))
                    {
                        debitCardResponseModel.CardBrand = "Master Card";
                    }
                    else
                    {
                        debitCardResponseModel.CardBrand = "Visa";
                    }

                    return StatusCode(200, debitCardResponseModel);
                }
            }
            else
            {
                _messageModel.Message = "You are not authorized to do such actions";
                return StatusCode(403, _messageModel);
            }
            _messageModel.Message = "You don't have a Debit Card!!";
            return StatusCode(400, _messageModel);
        }
        public async Task<ActionResult<MessageModel>> CreateDebitCard(ClaimsPrincipal currentUser, string username, BankAccounts bankAccount, BankSystemContext _context, Cards card, IBCryptPasswordHasherService _BCrypt, MessageModel _messageModel)
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
                Cards cardExists = null;
                BankAccounts bankAccountExists = null;

                if (userAuthenticate != null)
                {
                    cardExists = await _context.Cards.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                    bankAccountExists = await _context.BankAccounts.FirstOrDefaultAsync(x => x.Iban == bankAccount.Iban);
                }


                if (cardExists == null && bankAccountExists != null)
                {
                    if (ValidateUser(userAuthenticate))
                    {
                        card.UserId = userAuthenticate.Id;
                        card.BankAccountId = bankAccountExists.Id;
                        card.CardNumber = GenerateCardInfo.GenerateNumber(11);
                        var CVV = GenerateCardInfo.GenerateCVV(3);
                        card.Cvv = _BCrypt.HashPassword(CVV);
                        _context.Add(card);
                        await _context.SaveChangesAsync();
                        _messageModel.Message = "Debit Card created succesfully!";
                        return StatusCode(200, _messageModel);
                    }
                    else if (ValidateUser(userAuthenticate) == false)
                    {
                        _messageModel.Message = "User not found!";
                        return StatusCode(404, _messageModel);
                    }
                }

                _messageModel.Message = "User already has a Debit Card!";
                return StatusCode(400, _messageModel);
            }
            else
            {
                _messageModel.Message = "You are not autorized to do such actions!";
                return StatusCode(403, _messageModel);
            }
        }

        public async Task<ActionResult<MessageModel>> AddMoney(string cardNumber, string CVV, DateTime expireDate, ClaimsPrincipal currentUser, IBankAccountService _bankaccService, IBCryptPasswordHasherService _BCrypt, string username, decimal amount, BankSystemContext _context, ITransactionService _transactionService, MessageModel messageModel)
        {
            var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            BankAccounts bankAccountsExists = null;
            Cards cardsExists = null;

            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                if (userAuthenticate != null)
                {
                    bankAccountsExists = await _context.BankAccounts.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                    cardsExists = await _context.Cards.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                }
                else
                {
                    messageModel.Message = "User not found!";
                    return StatusCode(404, messageModel);
                }

                if (bankAccountsExists != null && cardsExists != null && (cardNumber == cardsExists.CardNumber && expireDate == cardsExists.CardExiprationDate && _BCrypt.AuthenticateDebitCardCVV(CVV, cardsExists)))
                {
                    if (cardsExists.CardExiprationDate < DateTime.Now)
                    {
                        messageModel.Message = "Debit Card is expired";
                        return StatusCode(406, messageModel);
                    }

                    await _bankaccService.AddMoney(bankAccountsExists, currentUser, username, amount, _context, _transactionService, messageModel);
                }
                else if (bankAccountsExists == null)
                {
                    messageModel.Message = "Bank Account not found";
                    return StatusCode(404, messageModel);
                }
                else if (cardsExists == null)
                {
                    messageModel.Message = "Debit Card not found";
                    return StatusCode(404, messageModel);
                }

            }
            messageModel.Message = "You are not autorized to do such actions!";
            return StatusCode(403, messageModel);
        }

        public async Task<ActionResult<MessageModel>> SimulatePurchase(string cardNumber, string CVV, DateTime expireDate, IBankAccountService _bankaccService, IBCryptPasswordHasherService _BCrypt, string product, ClaimsPrincipal currentUser, string username, decimal amount, string reciever, BankSystemContext _context, ITransactionService _transactionService, MessageModel messageModel)
        {
            var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            BankAccounts bankAccountsExists = null;
            Cards cardsExists = null;

            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                if (userAuthenticate != null)
                {
                    bankAccountsExists = await _context.BankAccounts.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                    cardsExists = await _context.Cards.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                }
                else
                {
                    messageModel.Message = "User not found!";
                    return StatusCode(404, messageModel);
                }

                if (bankAccountsExists != null && cardsExists != null && (cardNumber == cardsExists.CardNumber && expireDate == cardsExists.CardExiprationDate && _BCrypt.AuthenticateDebitCardCVV(CVV, cardsExists)))
                {
                    if (cardsExists.CardExiprationDate < DateTime.Now)
                    {
                        messageModel.Message = "Debit Card is expired";
                        return StatusCode(406, messageModel);
                    }

                    await _bankaccService.SimulatePurchase(bankAccountsExists, product, currentUser, username, amount, reciever, _context, _transactionService, messageModel);
                }
                else if (bankAccountsExists == null)
                {
                    messageModel.Message = "Bank Account not found";
                    return StatusCode(404, messageModel);
                }
                else if (cardsExists == null)
                {
                    messageModel.Message = "Debit Card not found";
                    return StatusCode(404, messageModel);
                }
            }

            messageModel.Message = "You are not autorized to do such actions!";
            return StatusCode(403, messageModel);
        }

        public async Task<ActionResult<MessageModel>> Withdraw(string cardNumber, string CVV, DateTime expireDate, IBankAccountService _bankaccService, IBCryptPasswordHasherService _BCrypt, ClaimsPrincipal currentUser, string username, decimal amount, string reciever, BankSystemContext _context, ITransactionService _transactionService, MessageModel messageModel)
        {
            var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            BankAccounts bankAccountsExists = null;
            Cards cardsExists = null;

            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                if (userAuthenticate != null)
                {
                    bankAccountsExists = await _context.BankAccounts.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                    cardsExists = await _context.Cards.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id && x.CardNumber == cardNumber && x.Cvv == CVV && x.CardExiprationDate == expireDate);
                }
                else
                {
                    messageModel.Message = "User not found!";
                    return StatusCode(404, messageModel);
                }

                if (bankAccountsExists != null && cardsExists != null && (cardNumber == cardsExists.CardNumber && expireDate == cardsExists.CardExiprationDate && _BCrypt.AuthenticateDebitCardCVV(CVV, cardsExists)))
                {
                    if (cardsExists.CardExiprationDate < DateTime.Now)
                    {
                        messageModel.Message = "Debit Card is expired";
                        return StatusCode(406, messageModel);
                    }

                    await _bankaccService.Withdraw(bankAccountsExists, currentUser, username, amount, reciever, _context, _transactionService, messageModel);
                }
                else if (bankAccountsExists == null)
                {
                    messageModel.Message = "Bank Account not found";
                    return StatusCode(404, messageModel);
                }
                else if (cardsExists == null)
                {
                    messageModel.Message = "Debit Card not found";
                    return StatusCode(404, messageModel);
                }
            }

            messageModel.Message = "You are not autorized to do such actions!";
            return StatusCode(403, messageModel);
        }
        public async Task<ActionResult<MessageModel>> DeleteDebitCard(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel _messageModel)
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
                Cards cardExists = null;

                if (user != null)
                {
                    cardExists = await _context.Cards.FirstOrDefaultAsync(x => x.UserId == user.Id);
                }

                if (user == null)
                {
                    _messageModel.Message = "User not found!";
                    return StatusCode(404, _messageModel);
                }
                else if (cardExists == null)
                {
                    _messageModel.Message = "User doesn't have a Debit Card!";
                    return StatusCode(400, _messageModel);
                }

                _context.Cards.Remove(cardExists);
                await _context.SaveChangesAsync();

                _messageModel.Message = $"Succsesfully deleted {user.Username} Debit Card!";
                return StatusCode(200, _messageModel);
            }
            else
            {
                _messageModel.Message = "You are not autorized to do such actions!";
                return StatusCode(403, _messageModel);
            }
        }

        private bool ValidateUser(Users user)
        {
            if (user != null)
            {
                return true;
            }
            return false;
        }

    }
}
