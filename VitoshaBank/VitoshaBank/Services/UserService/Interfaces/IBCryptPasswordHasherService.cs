using VitoshaBank.Data.Models;

namespace VitoshaBank.Services.Interfaces.UserService
{
    public interface IBCryptPasswordHasherService
    {
        public string HashPassword(string password);
        public bool AuthenticateUser(Users user, Users userDB);
        public bool AuthenticateWalletCVV(Wallets wallets, Wallets walletsDB);
    }
}
