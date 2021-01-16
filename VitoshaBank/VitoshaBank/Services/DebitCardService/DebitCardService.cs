using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;
using VitoshaBank.Data.ResponseModels;
using VitoshaBank.Services.DebitCardService.Interfaces;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;

namespace VitoshaBank.Services.DebitCardService
{
    public class DebitCardService : ControllerBase, IDebitCardService
    {

        public async Task<ActionResult<DebitCardResponseModel>> GetDebitCardInfo(ClaimsPrincipal currentUser, string username, BankSystemContext _context)
        {
            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
                BankAccounts bankAccountExits = null;
                Cards debitCardExists = null;
                DebitCardResponseModel debitCardResponseModel = new DebitCardResponseModel();

                if (userAuthenticate == null)
                {
                    return NotFound();
                }
                else
                {
                    bankAccountExits = await _context.BankAccounts.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                    debitCardExists = await _context.Cards.FirstOrDefaultAsync(x => x.BankAccountId == bankAccountExits.Id);
                }

                if (debitCardExists != null)
                {
                    debitCardResponseModel.IBAN = debitCardExists.CardNumber;
                    debitCardResponseModel.Amount = debitCardExists.Amount;
                    return Ok(debitCardResponseModel);
                }
            }
            return Ok("You don't have a debit card");
        }

        public async Task<ActionResult> CreateDebitCard(ClaimsPrincipal currentUser, string username, BankAccounts bankAccount, BankSystemContext _context, Cards card)
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
                        card.CardNumber = "1234 5678 90123";
                        card.Cvv = "123";
                        card.Amount = bankAccount.Amount;
                        _context.Add(card);
                        await _context.SaveChangesAsync();

                        return Ok();
                    }
                    else if (ValidateUser(userAuthenticate) == false)
                    {
                        return NotFound("User not found");
                    }
                    else if (ValidateCard(card) == false)
                    {
                        return BadRequest("Idiot don't put negative value!");
                    }
                }

                return BadRequest("User already has a card!");
            }
            else
            {
                return Unauthorized();
            }
        }

        public async Task<ActionResult<Users>> DeleteDebitCard(ClaimsPrincipal currentUser, string username, BankSystemContext _context)
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
                    return NotFound("Idiot no such user is found!");
                }
                else if (cardExists == null)
                {
                    return BadRequest("Dumbass, user doesn't have a debit card!");
                }

                _context.Cards.Remove(cardExists);
                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
        private bool ValidateCard(Cards card)
        {
            if (card.Amount < 0)
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
    }
}
