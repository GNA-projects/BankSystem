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
                    
                    return StatusCode(200,creditResponseModel);
                }
            }
            _messageModel.Message = "You don't have a credit";  
            return StatusCode(400, _messageModel);
        }
        public async Task<ActionResult> CreateCredit(ClaimsPrincipal currentUser, string username, Credits credits, IIBANGeneratorService _IBAN, BankSystemContext _context, ICalculateInterestService _interestService, MessageModel _messageModel)
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
                        //credits.Amount = 1000;
                        credits.Iban = _IBAN.GenerateIBANInVitoshaBank("Credit", _context);
                        credits.PaymentDate = DateTime.Now.AddMonths(1);
                        credits.CreditAmount = _interestService.CalculateCreditAmount(credits.Amount);
                        credits.Interest = 7.5m;
                        credits.Instalment = _interestService.CalculateInstalment(credits.CreditAmount, credits.Interest, 5);
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
        public async Task<ActionResult<Users>> DeleteCredit(ClaimsPrincipal currentUser, string username, BankSystemContext _context)
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
                    return NotFound("Idiot no such user is found!");
                }
                else if (creditsExists == null)
                {
                    return BadRequest("Dumbass, user doesn't have a deposits!");
                }

                _context.Credits.Remove(creditsExists);
                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return Unauthorized();
            }
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
    }
}
