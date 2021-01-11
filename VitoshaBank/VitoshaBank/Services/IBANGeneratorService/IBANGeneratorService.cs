using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;

namespace VitoshaBank.Services.IBANGeneratorService
{
    public static class IBANGeneratorService
    {
        //IBAN=BG 18 VITB 123456789 01 001
        

        public static string GenerateIBANInVitoshaBank(string BankAccountType) 
        {
            string countryCode = "BG";
            string uniqueNumber = "18";
            string bankBIC = "VITB";
            string secondUniqueNumber = "1234567";
            string bankAccountTypeCode = "";

            if (BankAccountType == "BankAccount")
            {
                bankAccountTypeCode = "01";
            }
            else if (BankAccountType == "Credit")
            {
                bankAccountTypeCode = "02";
            }
            else if (BankAccountType == "Deposit")
            {
                bankAccountTypeCode = "03";
            }
            else if(BankAccountType == "wallet")
            {
                bankAccountTypeCode = "04";
            }
            else
            {
                return null;
            }
            string currentBankAccountNumber = GetCurrentAvailabeAccountNumber(BankAccountType);
            string IBAN = $"{countryCode}{uniqueNumber}{bankBIC}{secondUniqueNumber}{bankAccountTypeCode}{currentBankAccountNumber}";
            return IBAN;
        }
        private static string GetCurrentAvailabeAccountNumber(string BankAccountType)
        {
            BankSystemContext dbContext = new BankSystemContext();
            if (BankAccountType == "BankAccount")
            {
                var lastBankAcc = dbContext.BankAccounts.LastOrDefault();
                var serialNumber = lastBankAcc.Id + 1;
                if (serialNumber >= 10 && serialNumber < 100)
                {
                    return $"0000{serialNumber}";
                }
                else if (serialNumber < 10)
                {
                    return $"00000{serialNumber}";
                }
               
                else if (serialNumber >= 100 && serialNumber < 1000)
                {
                    return $"000{serialNumber}";
                }
                else if (serialNumber >= 1000 && serialNumber < 10000)
                {
                    return $"00{serialNumber}";
                }
                else if (serialNumber >= 10000 && serialNumber < 100000)
                {
                    return $"0{serialNumber}";
                }
                else if (serialNumber >= 100000 && serialNumber < 1000000)
                {
                    return $"{serialNumber}";
                }
                else
                    return null;
            }
            else if (BankAccountType == "Credit")
            {
                var lastCredit = dbContext.Credits.LastOrDefault();
                var serialNumber = lastCredit.Id + 1;
                if (serialNumber >= 10 && serialNumber < 100)
                {
                    return $"0000{serialNumber}";
                }
                else if (serialNumber < 10)
                {
                    return $"00000{serialNumber}";
                }

                else if (serialNumber >= 100 && serialNumber < 1000)
                {
                    return $"000{serialNumber}";
                }
                else if (serialNumber >= 1000 && serialNumber < 10000)
                {
                    return $"00{serialNumber}";
                }
                else if (serialNumber >= 10000 && serialNumber < 100000)
                {
                    return $"0{serialNumber}";
                }
                else if (serialNumber >= 100000 && serialNumber < 1000000)
                {
                    return $"{serialNumber}";
                }
                else
                    return null;
            }
            else if (BankAccountType == "Deposit")
            {
                var lastDeposit = dbContext.Deposits.LastOrDefault();
                var serialNumber = lastDeposit.Id + 1;
                if (serialNumber >= 10 && serialNumber < 100)
                {
                    return $"0000{serialNumber}";
                }
                else if (serialNumber < 10)
                {
                    return $"00000{serialNumber}";
                }

                else if (serialNumber >= 100 && serialNumber < 1000)
                {
                    return $"000{serialNumber}";
                }
                else if (serialNumber >= 1000 && serialNumber < 10000)
                {
                    return $"00{serialNumber}";
                }
                else if (serialNumber >= 10000 && serialNumber < 100000)
                {
                    return $"0{serialNumber}";
                }
                else if (serialNumber >= 100000 && serialNumber < 1000000)
                {
                    return $"{serialNumber}";
                }
                else
                    return null;
            }
            else if(BankAccountType == "Wallet")
            {
                var lastWallet = dbContext.Wallets.LastOrDefault();
                var serialNumber = lastWallet.Id + 1;
                if (serialNumber >= 10 && serialNumber < 100)
                {
                    return $"0000{serialNumber}";
                }
                else if (serialNumber < 10)
                {
                    return $"00000{serialNumber}";
                }

                else if (serialNumber >= 100 && serialNumber < 1000)
                {
                    return $"000{serialNumber}";
                }
                else if (serialNumber >= 1000 && serialNumber < 10000)
                {
                    return $"00{serialNumber}";
                }
                else if (serialNumber >= 10000 && serialNumber < 100000)
                {
                    return $"0{serialNumber}";
                }
                else if (serialNumber >= 100000 && serialNumber < 1000000)
                {
                    return $"{serialNumber}";
                }
                else
                    return null;
            }
            else
            {
                return null;
            }
        }
    }
}
