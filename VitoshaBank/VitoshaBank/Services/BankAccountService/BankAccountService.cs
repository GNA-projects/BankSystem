using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;
using VitoshaBank.Services.BankAccountService.Interfaces;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;
using VitoshaBank.Services.DebitCardService;

namespace VitoshaBank.Services.BankAccountService
{
    public class BankAccountService : ControllerBase, IBankAccountService
    {
        public async Task<ActionResult> CreateBankAccount(ClaimsPrincipal currentUser, string username, BankAccounts bankAccount, IIBANGeneratorService _IBAN, BankSystemContext _context)
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
                BankAccounts bankAccountExists = null;

                if (userAuthenticate != null)
                {
                    bankAccountExists = await _context.BankAccounts.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                }


                if (bankAccountExists == null)
                {
                    if (ValidateUser(userAuthenticate) && ValidateBankAccount(bankAccount))
                    {
                        bankAccount.UserId = userAuthenticate.Id;
                        bankAccount.Iban = _IBAN.GenerateIBANInVitoshaBank("BankAccount", _context);
                        
                        _context.Add(bankAccount);
                        await _context.SaveChangesAsync();
                        Cards card = new Cards();
                        //bankAccount.Card = new Cards();
                        DebitCardService.DebitCardService debitCardService = new DebitCardService.DebitCardService();
                        await debitCardService.CreateDebitCard(currentUser, username, bankAccount, _context, card);
                        Cards newCard = await _context.Cards.FirstOrDefaultAsync(x => x.BankAccountId == bankAccount.Id);
                        
                        await _context.SaveChangesAsync();

                        return Ok();
                    }
                    else if (ValidateUser(userAuthenticate) == false)
                    {
                        return NotFound("User not found");
                    }
                    else if (ValidateBankAccount(bankAccount) == false)
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

        public async Task<ActionResult<Users>> DeleteBankAccount(ClaimsPrincipal currentUser, string username, BankSystemContext _context)
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
                BankAccounts bankAccountExists = null;

                if (user != null)
                {
                    bankAccountExists = await _context.BankAccounts.FirstOrDefaultAsync(x => x.UserId == user.Id);
                }

                if (user == null)
                {
                    return NotFound("Idiot no such user is found!");
                }
                else if (bankAccountExists == null)
                {
                    return BadRequest("Dumbass, user doesn't have a deposits!");
                }

                _context.BankAccounts.Remove(bankAccountExists);
                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
        private bool ValidateBankAccount(BankAccounts bankAccounts)
        {
            if (bankAccounts.Amount < 0)
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
