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
using VitoshaBank.Services.IBANGeneratorService.Interfaces;
using VitoshaBank.Services.Interfaces.WalletService;

namespace VitoshaBank.Services.WalletService
{
    public class WalletsService : ControllerBase, IWalletsService
    {
        public async Task<ActionResult<WalletResponseModel>> GetWalletInfo(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel _messageModel)
        {
            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
                Wallets walletExists = null;
                WalletResponseModel walletResponseModel = new WalletResponseModel();

                if (userAuthenticate == null)
                {
                    _messageModel.Message = "User not found!";
                    return StatusCode(404, _messageModel);
                }
                else
                {
                    walletExists = await _context.Wallets.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                }

                if (walletExists != null)
                {
                    walletResponseModel.IBAN = walletExists.Iban;
                    walletResponseModel.Amount = walletExists.Amount;
                    return StatusCode(200, walletResponseModel);
                }
            }
            else
            {
                _messageModel.Message = "You are not authorized to do such actions";
                return StatusCode(403, _messageModel);
            }

            _messageModel.Message = "You don't have a wallet!";
            return StatusCode(400, _messageModel);
        }
        public async Task<ActionResult<MessageModel>> CreateWallet(ClaimsPrincipal currentUser, string username, Wallets wallet, IIBANGeneratorService _IBAN, BankSystemContext _context, MessageModel _messageModel)
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
                Wallets walletExists = null;

                if (userAuthenticate != null)
                {
                    walletExists = await _context.Wallets.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                }


                if (walletExists == null)
                {
                    if (ValidateUser(userAuthenticate) && ValidateWallet(wallet))
                    {
                        wallet.UserId = userAuthenticate.Id;
                        wallet.Iban = _IBAN.GenerateIBANInVitoshaBank("Wallet", _context);
                        _context.Add(wallet);
                        await _context.SaveChangesAsync();

                        _messageModel.Message = "Wallet created succesfully!";
                        return StatusCode(200, _messageModel);
                    }
                    else if (ValidateUser(userAuthenticate) == false)
                    {
                        _messageModel.Message = "User not found!";
                        return StatusCode(404, _messageModel);
                    }
                    else if (ValidateWallet(wallet) == false)
                    {
                        _messageModel.Message = "Don't put negative value!";
                        return StatusCode(400, _messageModel);
                    }
                }
               
                _messageModel.Message = "User already has a wallet!";
                return StatusCode(400, _messageModel);
            }
            else
            {
                _messageModel.Message = "You are not autorized to do such actions!";
                return StatusCode(403, _messageModel);
            }
        }
        public async Task<ActionResult<MessageModel>> DepositMoney(Wallets wallet, ClaimsPrincipal currentUser, string username, decimal amount, BankSystemContext _context, MessageModel _messageModel)
        {
            var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            Wallets walletExists = null;
            BankAccounts bankAccounts = null;

            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                if (userAuthenticate != null)
                {
                    walletExists = await _context.Wallets.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id && x.Iban == wallet.Iban);
                }
                else
                {
                    _messageModel.Message = "User not found!";
                    return StatusCode(404, _messageModel);
                }

                if (walletExists != null)
                {
                    bankAccounts = _context.BankAccounts.FirstOrDefault(x => x.UserId == userAuthenticate.Id);
                    return await ValidateDepositAmountAndBankAccount(walletExists, amount, bankAccounts,_context, _messageModel);
                }
                else
                {
                    _messageModel.Message = "Wallet not found";
                    return StatusCode(404, _messageModel);
                }
            }

            _messageModel.Message = "You are not autorized to do such actions!";
            return StatusCode(403, _messageModel);
        }
        public async Task<ActionResult<MessageModel>> SimulatePurchase(Wallets wallet, string product, ClaimsPrincipal currentUser, string username, decimal amount, BankSystemContext _context, MessageModel _messageModel)
        {
            var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            Wallets walletExists = null;

            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                if (userAuthenticate != null)
                {
                    walletExists = await _context.Wallets.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id && x.Iban == wallet.Iban);
                }
                else
                {
                    _messageModel.Message = "User not found!";
                    return StatusCode(404, _messageModel);
                }

                if (walletExists != null)
                {
                    return await ValidatePurchaseAmountAndBankAccount(walletExists, product, amount, _context, _messageModel);
                }
                else
                {
                    _messageModel.Message = "Wallet not found";
                    return StatusCode(404, _messageModel);
                }
            }
            _messageModel.Message = "You are not autorized to do such actions!";
            return StatusCode(403, _messageModel);
        }
        public async Task<ActionResult<MessageModel>> DeleteWallet(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel _messageModel)
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
                Wallets walletExists = null;

                if (user != null)
                {
                    walletExists = await _context.Wallets.FirstOrDefaultAsync(x => x.UserId == user.Id);
                }

                if (user == null)
                {
                    _messageModel.Message = "User not found";
                    return StatusCode(404, _messageModel);
                }
                else if (walletExists == null)
                {
                    _messageModel.Message = "User doesn't have a wallet";
                    return StatusCode(400, _messageModel);
                }

                _context.Wallets.Remove(walletExists);
                await _context.SaveChangesAsync();

                _messageModel.Message = "Wallet deleted succesfully!";
                return StatusCode(200, _messageModel);
            }
            else
            {
                _messageModel.Message = "You are not autorized to do such actions!";
                return StatusCode(403, _messageModel);
            }
        }

        private bool ValidateWallet(Wallets wallet)
        {
            if (wallet.Amount < 0)
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

        private async Task<ActionResult> ValidateDepositAmountAndBankAccount(Wallets walletExists, decimal amount, BankAccounts bankAccount, BankSystemContext _context, MessageModel _messageModel)
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
                    walletExists.Amount = walletExists.Amount + amount;
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
        private async Task<ActionResult> ValidatePurchaseAmountAndBankAccount(Wallets walletExists, string product, decimal amount, BankSystemContext _context, MessageModel _messageModel)
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
                if (walletExists.Amount < amount)
                {
                    _messageModel.Message = "You don't have enough money in wallet!";
                    return StatusCode(406, _messageModel);
                }
                else
                {
                    walletExists.Amount = walletExists.Amount - amount;
                    await _context.SaveChangesAsync();
                }
            }

            _messageModel.Message = $"Succesfully purhcased {product}.";
            return StatusCode(200, _messageModel);
        }
    }
}
