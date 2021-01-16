﻿using VitoshaBank.Data.Models;

namespace VitoshaBank.Services.Interfaces.UserService
{
    public interface IBCryptPasswordHasherService
    {
        public string HashPassword(string password);
        public bool Authenticate(Users user, Users userDB);
    }
}