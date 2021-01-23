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
using VitoshaBank.Services.CalculateDividendService;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;

namespace VitoshaBank.Services.DepositService
{
    public class DepositService : ControllerBase, IDepositService
    {
        public async Task<ActionResult<DepositResponseModel>> GetDepositInfo(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel _messageModel)
        {
            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
                Deposits depositExists = null;
                DepositResponseModel depositResponseModel = new DepositResponseModel();

                if (userAuthenticate == null)
                {
                    _messageModel.Message = "User not found";
                    return StatusCode(404, _messageModel);
                }
                else
                {
                    depositExists = await _context.Deposits.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                }

                if (depositExists != null)
                {
                    depositResponseModel.IBAN = depositExists.Iban;
                    depositResponseModel.Amount = depositExists.Amount;
                    depositResponseModel.PaymentDate = depositExists.PaymentDate;
                    return StatusCode(200, depositResponseModel);
                }
            }
            else
            {
                _messageModel.Message = "You are not authorized to do such actions";
                return StatusCode(403, _messageModel);
            }

            _messageModel.Message = "You don't have a deposit!";
            return StatusCode(400, _messageModel);
        }
        public async Task<ActionResult<MessageModel>> CreateDeposit(ClaimsPrincipal currentUser, string username, Deposits deposits, IIBANGeneratorService _IBAN, BankSystemContext _context, MessageModel _messageModel)
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
                else
                {
                    _messageModel.Message = "User not found";
                    return StatusCode(404, _messageModel);
                }


                if (depositExists == null)
                {
                    if (ValidateUser(userAuthenticate) && ValidateDeposits(deposits))
                    {
                        deposits.UserId = userAuthenticate.Id;
                        //deposits.TermOfPayment = 6;
                        deposits.Iban = _IBAN.GenerateIBANInVitoshaBank("Deposit", _context);
                        deposits.PaymentDate = DateTime.Now.AddMonths(deposits.TermOfPayment);
                        //deposits.Amount = 45;
                        deposits.Divident = CalculateDivident.GetDividentPercent(deposits.Amount, deposits.TermOfPayment);
                        _context.Add(deposits);
                        await _context.SaveChangesAsync();

                        _messageModel.Message = "Deposit created succesfully";
                        return StatusCode(200, _messageModel);
                    }
                    else if (ValidateUser(userAuthenticate) == false)
                    {
                        _messageModel.Message = "User not found";
                        return StatusCode(404, _messageModel);
                    }
                    else if (ValidateDeposits(deposits) == false)
                    {
                        _messageModel.Message = "Don't put negative value!";
                        return StatusCode(400, _messageModel);
                    }
                }

                _messageModel.Message = "User already has deposit!";
                return StatusCode(400, _messageModel);
            }
            else
            {
                _messageModel.Message = "You are not authorized to do such actions";
                return StatusCode(403, _messageModel);
            }
        }

        public async Task<ActionResult<MessageModel>> DeleteDeposit(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel _messageModel)
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
                    _messageModel.Message = "User not found!";
                    return StatusCode(404, _messageModel);
                }
                else if (depositsExists == null)
                {
                    _messageModel.Message = "User doesn't have a deposit";
                    return StatusCode(400, _messageModel);
                }

                _context.Deposits.Remove(depositsExists);
                await _context.SaveChangesAsync();

                _messageModel.Message = $"Succsesfully deleted {user.Username} deposit!";
                return StatusCode(200, _messageModel);
            }
            else
            {
                _messageModel.Message = "You are not authorized to do such actions";
                return StatusCode(403, _messageModel);
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
