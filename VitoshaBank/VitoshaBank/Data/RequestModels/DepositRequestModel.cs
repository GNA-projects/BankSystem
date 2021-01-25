using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;

namespace VitoshaBank.Data.RequestModels
{
    public class DepositRequestModel
    {
        public Deposits Deposit { get; set; }

        public string Username { get; set; }
        public BankAccounts BankAccount { get;  set; }
        public decimal Amount { get; set; }
    }
}
