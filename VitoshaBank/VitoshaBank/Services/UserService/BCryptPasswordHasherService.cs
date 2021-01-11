using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using VitoshaBank.Data.Models;
using VitoshaBank.Services.Interfaces;

namespace VitoshaBank.Services
{
    public class BCryptPasswordHasherService : IBCryptPasswordHasherService
    {
        public BCryptPasswordHasherService(IOptions<PasswordHasherOptions> optionsAccessor = null)
        {

        }
        public string HashPassword(string password)
        {
            string advancedHashedPassword = password + "NeverGonnaLetYouDown";
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            return BCrypt.Net.BCrypt.HashPassword(advancedHashedPassword, salt);
        }

        public bool Authenticate(Users user, Users userDB)
        {
            // check user found and verify password
            if (!BCrypt.Net.BCrypt.Verify(user.Password + "NeverGonnaLetYouDown", userDB.Password))
            {
                // authentication failed
                return false;
            }
            else
            {
                // authentication successful
                return true;
            }
        }

    }
}
