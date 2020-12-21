using System;
using System.Collections.Generic;

#nullable disable

namespace VitoshaBank.Data.Models
{
    public partial class Card
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CardNumber { get; set; }
        public int Cvv { get; set; }
        public int BankAccountId { get; set; }
        public decimal Amount { get; set; }

        public virtual BankAccount BankAccountNavigation { get; set; }
        public virtual User User { get; set; }
        public virtual BankAccount BankAccount { get; set; }
    }
}
