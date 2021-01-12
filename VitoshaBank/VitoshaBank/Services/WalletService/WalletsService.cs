using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;
using VitoshaBank.Services.Interfaces.WalletService;

namespace VitoshaBank.Services.WalletService
{
    public class WalletsService : ControllerBase, IWalletsService
    {
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

    }
}
