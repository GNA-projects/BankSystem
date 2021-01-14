using System;
using System.Collections.Generic;

namespace VitoshaBank.Data.Models
{
    public partial class Cards
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CardNumber { get; set; }
        public string Cvv { get; set; }
        public int BankAccountId { get; set; }
        public decimal Amount { get; set; }

        public virtual BankAccounts BankAccount { get; set; }
        public virtual Users User { get; set; }
    }
}
