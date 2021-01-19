using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;
using VitoshaBank.Services.BankAccountService.Interfaces;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;
using VitoshaBank.Services.DebitCardService;
using VitoshaBank.Services.DebitCardService.Interfaces;
using VitoshaBank.Data.ResponseModels;
using VitoshaBank.Data.MessageModels;
using System;

namespace VitoshaBank.Services.BankAccountService
{
    public class BankAccountService : ControllerBase, IBankAccountService
    {
        public async Task<ActionResult<BankAccountResponseModel>> GetBankAccountInfo(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel messageModel)
        {
            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
                BankAccounts bankAccountExists = null;
                BankAccountResponseModel bankAccountResponseModel = new BankAccountResponseModel();

                if (userAuthenticate == null)
                {
                    messageModel.Message = "User not found";
                    return StatusCode(404, messageModel);
                }
                else
                {
                    bankAccountExists = await _context.BankAccounts.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                }

                if (bankAccountExists != null)
                {
                    bankAccountResponseModel.IBAN = bankAccountExists.Iban;
                    bankAccountResponseModel.Amount = bankAccountExists.Amount;
                    return StatusCode(200, bankAccountResponseModel);
                }
            }
            else
            {
                messageModel.Message = "Invalid token!";
                return StatusCode(401, messageModel);
            }

            messageModel.Message = "You don't have a bank account!";
            return StatusCode(400, messageModel);
        }
        public async Task<ActionResult> CreateBankAccount(ClaimsPrincipal currentUser, string username, BankAccounts bankAccount, IIBANGeneratorService _IBAN, BankSystemContext _context, IDebitCardService _debitCardService, MessageModel messageModel)
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
                        await _debitCardService.CreateDebitCard(currentUser, username, bankAccount, _context, card);
                        messageModel.Message = "Bank Account created succesfully";
                        return StatusCode(201, messageModel);
                    }
                    else if (ValidateUser(userAuthenticate) == false)
                    {
                        messageModel.Message = "User not found!";
                        return StatusCode(404, messageModel);
                    }
                    else if (ValidateBankAccount(bankAccount) == false)
                    {
                        messageModel.Message = "Invalid parameteres!";
                        return StatusCode(400, messageModel);
                    }
                }
                messageModel.Message = "BankAccount already exists!";
                return StatusCode(400, messageModel);
            }
            else
            {
                messageModel.Message = "You are not authorized to do such actions";
                return StatusCode(403, messageModel);
            }
        }
        public async Task<ActionResult> DepositMoney(BankAccounts bankAccount, ClaimsPrincipal currentUser, string username, decimal amount, BankSystemContext _context, MessageModel messageModel)
        {
            var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
           
            BankAccounts bankAccounts = null;

            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                if (userAuthenticate != null)
                {
                    bankAccounts = await _context.BankAccounts.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                }
                else
                {
                    return NotFound("No such user exists");
                }

                if (bankAccounts != null)
                {
                    if (ValidateDepositAmountBankAccount(amount,bankAccounts,_context))
                    {
                        bankAccount.Amount = bankAccount.Amount + amount;
                        await _context.SaveChangesAsync();
                        messageModel.Message = "Money deposited succesfully!";
                        return StatusCode(200, messageModel);
                    }
                    messageModel.Message = "Invalid deposit amount!";
                    return StatusCode(400, messageModel);
                }
                else
                {
                    messageModel.Message = "BankAccount not found";
                    return StatusCode(404, messageModel);
                }
            }
            messageModel.Message = "You are not autorized to do such actions!";
            return StatusCode(403, messageModel);
        }
        public async Task<ActionResult> SimulatePurchase(BankAccounts bankAccount, string product, ClaimsPrincipal currentUser, string username, decimal amount,string reciever, BankSystemContext _context, MessageModel messageModel)
        {
            var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            BankAccounts bankAccounts = null;

            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                if (userAuthenticate != null)
                {
                    bankAccounts = await _context.BankAccounts.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                }
                else
                {
                    return NotFound("No such user exists");
                }

                if (bankAccounts != null)
                {
                    if (ValidateDepositAmountBankAccount(amount, bankAccounts, _context))
                    {
                        bankAccount.Amount = bankAccount.Amount - amount;
                        //Transaction transaction = new transactiom"
                        await _context.SaveChangesAsync();
                        messageModel.Message = "Money paied succesfully!";
                        return StatusCode(200, messageModel);
                    }
                    messageModel.Message = "Invalid payment amount!";
                    return StatusCode(400, messageModel);
                }
                else
                {
                    messageModel.Message = "BankAccount not found";
                    return StatusCode(404, messageModel);
                }
            }
            messageModel.Message = "You are not autorized to do such actions!";
            return StatusCode(403, messageModel);
        }
        public async Task<ActionResult<Users>> DeleteBankAccount(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel messageModel)
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
                Cards cardExists = null;
                if (user != null)
                {
                    bankAccountExists = await _context.BankAccounts.FirstOrDefaultAsync(x => x.UserId == user.Id);
                    cardExists = await _context.Cards.FirstOrDefaultAsync(x => x.BankAccountId == bankAccountExists.Id);
                }

                if (user == null)
                {
                    messageModel.Message = "User not found!";
                    return StatusCode(404, messageModel);
                }
                else if (bankAccountExists == null)
                {
                    messageModel.Message = "User doesn't have a bank account!";
                    return StatusCode(400, messageModel);
                }
                else if (cardExists == null)
                {
                    messageModel.Message = "No debit card found!";
                    return StatusCode(400, messageModel);
                }
                else
                {
                    _context.Cards.Remove(cardExists);
                    _context.BankAccounts.Remove(bankAccountExists);
                    await _context.SaveChangesAsync();

                    messageModel.Message = $"Succsesfully deleted {user.Username} bank account and debit card!";
                    return StatusCode(200, messageModel);

                }
            }
            else
            {
                messageModel.Message = "You are not authorized to do such actions";
                return StatusCode(403, messageModel);
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
        private bool ValidateDepositAmountBankAccount( decimal amount, BankAccounts bankAccounts, BankSystemContext context)
        {
            if (amount>0)
            {
                return true;
            }
            return false;
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

