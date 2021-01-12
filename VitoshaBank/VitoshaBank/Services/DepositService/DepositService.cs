using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;
using VitoshaBank.Services.CalculateDividendService;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;

namespace VitoshaBank.Services.DepositService
{
    public class DepositService : ControllerBase
    {
        public async Task<ActionResult> CreateDeposit(ClaimsPrincipal currentUser, string username, Deposits deposits, IIBANGeneratorService _IBAN, BankSystemContext _context)
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
                Deposits depositExists = null;

                if (userAuthenticate != null)
                {
                    depositExists = await _context.Deposits.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                }


                if (depositExists == null)
                {
                    if (ValidateUser(userAuthenticate) && ValidateDeposits(deposits))
                    {
                        deposits.UserId = userAuthenticate.Id;
                        deposits.Iban = _IBAN.GenerateIBANInVitoshaBank("Deposit", _context);
                        deposits.PaymentDate = DateTime.Now.AddMonths(deposits.TermOfPayment);
                        deposits.Divident = CalculateDividentService.GetDividentPercent(deposits.Amount, deposits.TermOfPayment);
                        _context.Add(deposits);
                        await _context.SaveChangesAsync();

                        return Ok();
                    }
                    else if (ValidateUser(userAuthenticate) == false)
                    {
                        return NotFound("User not found");
                    }
                    else if (ValidateDeposits(deposits) == false)
                    {
                        return BadRequest("Idiot don't put negative value!");
                    }
                }

                return BadRequest("User already has a Deposit!");
            }
            else
            {
                return Unauthorized();
            }
        }

        public async Task<ActionResult<Users>> DeleteDeposit(ClaimsPrincipal currentUser, string username, BankSystemContext _context)
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
                Deposits depositsExists = null;

                if (user != null)
                {
                    depositsExists = await _context.Deposits.FirstOrDefaultAsync(x => x.UserId == user.Id);
                }

                if (user == null)
                {
                    return NotFound("Idiot no such user is found!");
                }
                else if (depositsExists == null)
                {
                    return BadRequest("Dumbass, user doesn't have a deposits!");
                }

                _context.Deposits.Remove(depositsExists);
                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
        private bool ValidateDeposits(Deposits deposit)
        {
            if (deposit.Amount < 0)
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
