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
using VitoshaBank.Services.GenerateCardInfoService;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;
using VitoshaBank.Services.Interfaces.UserService;
using VitoshaBank.Services.Interfaces.WalletService;
using VitoshaBank.Services.TransactionService.Interfaces;

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
                    walletResponseModel.Amount = Math.Round(walletExists.Amount, 2);
                    walletResponseModel.CardNumber = walletExists.CardNumber;
                    if (walletResponseModel.CardNumber.StartsWith('5'))
                    {
                        walletResponseModel.CardBrand = "Master Card";
                    }
                    else
                    {
                        walletResponseModel.CardBrand = "Visa";
                    }
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
        public async Task<ActionResult<MessageModel>> CreateWallet(ClaimsPrincipal currentUser, string username, Wallets wallet, IIBANGeneratorService _IBAN, IBCryptPasswordHasherService _BCrypt, BankSystemContext _context, MessageModel _messageModel)
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
                        wallet.CardNumber = GenerateCardInfo.GenerateNumber(11);
                        var CVV = GenerateCardInfo.GenerateCVV(3);
                        wallet.Cvv = _BCrypt.HashPassword(CVV);
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
        public async Task<ActionResult<MessageModel>> AddMoney(Wallets wallet, ClaimsPrincipal currentUser, string username, decimal amount, BankSystemContext _context, ITransactionService _transation, MessageModel _messageModel)
        {
            var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            Wallets walletExists = null;
            ChargeAccounts bankAccounts = null;

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
                    if (walletExists.CardExipirationDate < DateTime.Now)
                    {
                        _messageModel.Message = "Wallet Card is expired";
                        return StatusCode(406, _messageModel);
                    }

                    bankAccounts = _context.ChargeAccounts.FirstOrDefault(x => x.UserId == userAuthenticate.Id);
                    return await ValidateDepositAmountAndBankAccount(userAuthenticate, currentUser, walletExists, amount, bankAccounts, _context, _transation, _messageModel);
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
        public async Task<ActionResult<MessageModel>> SimulatePurchase(Wallets wallet, string product, string reciever, ClaimsPrincipal currentUser, string username, decimal amount, BankSystemContext _context, ITransactionService _transation, IBCryptPasswordHasherService _BCrypt, MessageModel _messageModel)
        {
            var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            Wallets walletExists = null;

            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                if (userAuthenticate != null)
                {
                    walletExists = await _context.Wallets.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                    if (walletExists != null && (wallet.CardNumber == walletExists.CardNumber && wallet.CardExipirationDate == walletExists.CardExipirationDate && _BCrypt.AuthenticateWalletCVV(wallet, walletExists))) 
                    {
                        if (walletExists.CardExipirationDate < DateTime.Now)
                        {
                            _messageModel.Message = "Wallet Card is expired";
                            return StatusCode(406, _messageModel);
                        }

                        return await ValidatePurchaseAmountAndBankAccount(userAuthenticate, currentUser, walletExists, product, reciever, amount, _context, _transation, _messageModel);
                    }
                    else
                    {
                        _messageModel.Message = "Wallet not found";
                        return StatusCode(404, _messageModel);
                    }
                }
                else
                {
                    _messageModel.Message = "User not found!";
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
                    _messageModel.Message = "User doesn't have a Wallet";
                    return StatusCode(400, _messageModel);
                }

                _context.Wallets.Remove(walletExists);
                await _context.SaveChangesAsync();

                _messageModel.Message = $"Succsesfully deleted {user.Username} Wallet!";
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

        private async Task<ActionResult> ValidateDepositAmountAndBankAccount(Users userAuthenticate, ClaimsPrincipal currentUser, Wallets walletExists, decimal amount, ChargeAccounts bankAccount, BankSystemContext _context, ITransactionService _transation, MessageModel _messageModel)
        {
            if (amount < 0)
            {
                _messageModel.Message = "Invalid payment amount!";
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
                    Transactions transaction = new Transactions();
                    transaction.SenderAccountInfo = bankAccount.Iban;
                    transaction.RecieverAccountInfo = walletExists.Iban;

                    await _transation.CreateTransaction(userAuthenticate, currentUser, amount, transaction, "Depositing money in Wallet", _context, _messageModel);
                    await _context.SaveChangesAsync();
                }
                else if (bankAccount.Amount < amount)
                {
                    _messageModel.Message = "You don't have enough money in Bank Account!";
                    return StatusCode(406, _messageModel);
                }
                else if (bankAccount == null)
                {
                    _messageModel.Message = "You don't have a bank account";
                    return StatusCode(400, _messageModel);
                }
            }
            _messageModel.Message = $"Succesfully deposited {amount} leva in Wallet.";
            return StatusCode(200, _messageModel);
        }
        private async Task<ActionResult> ValidatePurchaseAmountAndBankAccount(Users userAuthenticate, ClaimsPrincipal currentUser, Wallets walletExists, string product, string reciever, decimal amount, BankSystemContext _context, ITransactionService _transation, MessageModel _messageModel)
        {
            if (amount < 0)
            {
                _messageModel.Message = "Invalid payment amount!";
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
                    Transactions transaction = new Transactions();
                    walletExists.Amount = walletExists.Amount - amount;
                    transaction.SenderAccountInfo = walletExists.Iban;
                    transaction.RecieverAccountInfo = reciever;
                    await _transation.CreateTransaction(userAuthenticate, currentUser, amount, transaction, $"Purchasing {product} with Wallet", _context, _messageModel);
                    await _context.SaveChangesAsync();
                }
            }

            _messageModel.Message = $"Succesfully purhcased {product}.";
            return StatusCode(200, _messageModel);
        }
    }
}
