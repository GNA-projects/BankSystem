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
using VitoshaBank.Services.DebitCardService.Interfaces;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;

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
                    debitCardResponseModel.IBAN = debitCardExists.CardNumber;
                    
                    return Ok(debitCardResponseModel);
                }
            }
            _messageModel.Message = "You don't have a debit card!!";
            return StatusCode(400,"You don't have a debit card");
        }
        public async Task<ActionResult> CreateDebitCard(ClaimsPrincipal currentUser, string username, BankAccounts bankAccount, BankSystemContext _context, Cards card, MessageModel _messageModel)
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
                    if (ValidateUser(userAuthenticate) && ValidateCard(card))
                    {
                        card.UserId = userAuthenticate.Id;
                        card.BankAccountId = bankAccount.Id;
                        card.CardNumber = GenerateNumber(15);
                        card.Cvv = GenerateCVV(3);
                        card.CardExiprationDate = DateTime.Now.AddMonths(60);
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
                    else if (ValidateCard(card) == false)
                    {
                        _messageModel.Message = "Invalid parameteres!";
                        return StatusCode(400, _messageModel);
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
        public async Task<ActionResult<Users>> DeleteDebitCard(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel _messageModel)
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
        private string GenerateCVV(int number)
        {
            Random random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, number)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private bool ValidateCard(Cards card)
        {
            if (card.BankAccountId ==-1)
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
        private static string GenerateNumber(int number)
        {
            Random random = new Random();
            const string chars = "0123456789";
            var serial = (Enumerable.Repeat(chars, number)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            Random random1 = new Random();
            const string nums = "45";
            var type = (Enumerable.Repeat(nums, 1)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return $"{string.Join("", type)}{string.Join("",serial)}";
        }
    }
}
