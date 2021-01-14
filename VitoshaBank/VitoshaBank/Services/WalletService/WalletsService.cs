using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;
using VitoshaBank.Data.ResponseModels;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;
using VitoshaBank.Services.Interfaces.WalletService;

namespace VitoshaBank.Services.WalletService
{
    public class WalletsService : ControllerBase, IWalletsService
    {
        public async Task<ActionResult<WalletResponseModel>> GetWalletInfo(ClaimsPrincipal currentUser, string username, BankSystemContext _context)
        {
            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
                Wallets walletExists = null;
                WalletResponseModel walletResponseModel = new WalletResponseModel();

                if (userAuthenticate == null)
                {
                    return NotFound();
                }
                else
                {
                    walletExists = await _context.Wallets.FirstOrDefaultAsync(x => x.UserId == userAuthenticate.Id);
                }

                if (walletExists != null)
                {
                    walletResponseModel.CardNumber = walletExists.Iban;
                    walletResponseModel.Amount = walletExists.Amount;
                    return Ok(walletResponseModel);
                }
            }
            return Ok("You don't have a wallet");
        }
        public async Task<ActionResult> CreateWallet(ClaimsPrincipal currentUser, string username, Wallets wallet, IIBANGeneratorService _IBAN, BankSystemContext _context)
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

                        return Ok();
                    }
                    else if (ValidateUser(userAuthenticate) == false)
                    {
                        return NotFound("Idiot no such user is found!");
                    }
                    else if (ValidateWallet(wallet) == false)
                    {
                        return BadRequest("Idiot don't put negative value!");
                    }
                }

                return BadRequest("User already has a wallet!");
            }
            else
            {
                return Unauthorized();
            }
        }

        public async Task<ActionResult> DepositMoney(Wallets wallet, ClaimsPrincipal currentUser, string username, decimal amount, BankSystemContext _context)
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
                    return NotFound("No such user exists");
                }

                if (walletExists != null)
                {
                    return await ValidateDepositAmountAndBankAccount(walletExists, amount, _context);
                }
                else
                {
                    return Ok("You don't have a wallet with such IBAN");
                }
            }

            return Unauthorized();
        }

        public async Task<ActionResult> SimulatePurchase(Wallets wallet, string product, ClaimsPrincipal currentUser, string username, decimal amount, BankSystemContext _context)
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
                    return NotFound("No such user exists");
                }

                if (walletExists != null)
                {
                    return await ValidatePurchaseAmountAndBankAccount(walletExists, product, amount, _context);
                }
                else
                {
                    return Ok("You don't have a wallet with such IBAN");
                }
            }

            return Unauthorized();
        }
        public async Task<ActionResult<Users>> DeleteWallet(ClaimsPrincipal currentUser, string username, BankSystemContext _context)
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
                    return NotFound("Idiot no such user is found!");
                }
                else if (walletExists == null)
                {
                    return BadRequest("Dumbass, user doesn't have a wallet!");
                }

                _context.Wallets.Remove(walletExists);
                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return Unauthorized();
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

        private async Task<ActionResult> ValidateDepositAmountAndBankAccount(Wallets walletExists, decimal amount, BankSystemContext _context)
        {
            if (amount < 0)
            {
                return BadRequest("Smart...Don't put negative amount!");
            }
            else if (amount == 0)
            {
                return BadRequest("Put amount more than 0.00lv");
            }
            else
            {
                walletExists.Amount = walletExists.Amount + amount;
                await _context.SaveChangesAsync();
                //TODO: GET MONEY FROM BANKACCOUNT !!!!!!!!!!!!!!!
                //TODO: VALIDATE IF BANKACCOUNT HAS MONEY !!!!
            }

            return Ok($"Succesfully deposited {amount} leva.");
        }
        private async Task<ActionResult> ValidatePurchaseAmountAndBankAccount(Wallets walletExists, string product, decimal amount, BankSystemContext _context)
        {
            if (amount < 0)
            {
                return BadRequest("Smart...Don't put negative amount!");
            }
            else if (amount == 0)
            {
                return BadRequest("Put amount more than 0.00lv");
            }
            else
            {
                if (walletExists.Amount < amount)
                {
                    return StatusCode(406, "You don't have enough money for this purchase!!!");
                }
                else
                {
                    walletExists.Amount = walletExists.Amount - amount;
                    await _context.SaveChangesAsync();
                }
            }

            return Ok($"Succesfully purchased {product} that cost {amount} leva.");
        }
    }
}
