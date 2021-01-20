using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;

namespace VitoshaBank.Data.RequestModels
{
    public class BankAccountRequestModel
    {
        public BankAccounts BankAccount { get; set; }
        public string Username { get; set; }
        public string Product { get; internal set; }
        public decimal Amount { get; internal set; }
        public string Reciever { get; internal set; }
    }
}
