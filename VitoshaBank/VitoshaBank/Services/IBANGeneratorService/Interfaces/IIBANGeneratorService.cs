using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;

namespace VitoshaBank.Services.IBANGeneratorService.Interfaces
{
    public interface IIBANGeneratorService
    {
        public string GenerateIBANInVitoshaBank(string BankAccountType, BankSystemContext dbContext);
    }
}
