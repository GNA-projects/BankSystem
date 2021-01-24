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
using VitoshaBank.Services.IBANGeneratorService.Interfaces;
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

                    return StatusCode(200, debitCardResponseModel);
                }
            }
            else
            {
                _messageModel.Message = "You are not authorized to do such actions";
                return StatusCode(403, _messageModel);
            }
            _messageModel.Message = "You don't have a debit card!!";
            return StatusCode(400, _messageModel);
        }
        public async Task<ActionResult<MessageModel>> CreateDebitCard(ClaimsPrincipal currentUser, string username, BankAccounts bankAccount, BankSystemContext _context, Cards card, MessageModel _messageModel)
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

                if (userAuthenticate != null)
                {
                    cardExists = await _context.Cards.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                }


                if (cardExists == null)
                {
                    if (ValidateUser(userAuthenticate))
                    {
                        card.UserId = userAuthenticate.Id;
                        card.BankAccountId = bankAccount.Id;
                        _context.Add(card);
                        await _context.SaveChangesAsync();
                        _messageModel.Message = "DebitCard created succesfully!";
                        return StatusCode(200, _messageModel);
                    }
                    else if (ValidateUser(userAuthenticate) == false)
                    {
                        _messageModel.Message = "User not found!";
                        return StatusCode(404, _messageModel);
                    }
                }

                _messageModel.Message = "User already has a DebitCard!";
                return StatusCode(400, _messageModel);
            }
            else
            {
                _messageModel.Message = "You are not autorized to do such actions!";
                return StatusCode(403, _messageModel);
            }
        }

        public async Task<ActionResult<MessageModel>> DepositMoney(string cardNumber, ClaimsPrincipal currentUser, IBankAccountService _bankaccService,string username, decimal amount, BankSystemContext _context,ITransactionService _transactionService, MessageModel messageModel)
        {
            var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            BankAccounts bankAccounts = null;
            Cards cards = null;

            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                if (userAuthenticate != null)
                {
                    bankAccounts = await _context.BankAccounts.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                    cards = await _context.Cards.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id && x.CardNumber == cardNumber);
                }
                else
                {
                    messageModel.Message = "User not found!";
                    return StatusCode(404, messageModel);
                }

                if (bankAccounts != null && cards != null)
                {
                    if (cards.CardExiprationDate < DateTime.Now)
                    {
                        messageModel.Message = "DebitCard is expired";
                        return StatusCode(406, messageModel);
                    }
                    await _bankaccService.DepositMoney(bankAccounts, currentUser, username, amount, _context,_transactionService, messageModel);
                }
                else if (bankAccounts == null)
                {
                    messageModel.Message = "BankAccount not found";
                    return StatusCode(404, messageModel);
                }
                else if (cards == null)
                {
                    messageModel.Message = "DebitCard not found";
                    return StatusCode(404, messageModel);
                }
                
            }
            messageModel.Message = "You are not autorized to do such actions!";
            return StatusCode(403, messageModel);
        }

        public async Task<ActionResult<MessageModel>> SimulatePurchase(string cardNumber, IBankAccountService _bankaccService, string product, ClaimsPrincipal currentUser, string username, decimal amount, string reciever, BankSystemContext _context, ITransactionService _transactionService, MessageModel messageModel)
        {
            var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            BankAccounts bankAccounts = null;
            Cards cards = null;

            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                if (userAuthenticate != null)
                {
                    bankAccounts = await _context.BankAccounts.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                    cards = await _context.Cards.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id && x.CardNumber == cardNumber);
                }
                else
                {
                    messageModel.Message = "User not found!";
                    return StatusCode(404, messageModel);
                }

                if (bankAccounts != null && cards != null)
                {
                    if (cards.CardExiprationDate < DateTime.Now)
                    {
                        messageModel.Message = "DebitCard is expired";
                        return StatusCode(406, messageModel);
                    }
                    await _bankaccService.SimulatePurchase(bankAccounts, product, currentUser, username, amount, reciever, _context,_transactionService, messageModel);
                }
                else if(bankAccounts == null)
                {
                    messageModel.Message = "BankAccount not found";
                    return StatusCode(404, messageModel);
                }
                else if (cards == null)
                {
                    messageModel.Message = "DebitCard not found";
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
                    _messageModel.Message = "User doesn't have a DebitCard!";
                    return StatusCode(400, _messageModel);
                }

                _context.Cards.Remove(cardExists);
                await _context.SaveChangesAsync();

                _messageModel.Message = "DebitCard deleted succesfully!";
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
