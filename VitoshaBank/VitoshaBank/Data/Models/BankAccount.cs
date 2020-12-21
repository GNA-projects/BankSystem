using System;
using System.Collections.Generic;

#nullable disable

namespace VitoshaBank.Data.Models
{
    public partial class BankAccount
    {
        public BankAccount()
        {
            TransactionRecieverAccounts = new HashSet<Transaction>();
            TransactionSenderAccounts = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string Iban { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public int CardId { get; set; }

        public virtual Card Card { get; set; }
        public virtual User User { get; set; }
        public virtual Card CardNavigation { get; set; }
        public virtual ICollection<Transaction> TransactionRecieverAccounts { get; set; }
        public virtual ICollection<Transaction> TransactionSenderAccounts { get; set; }
    }
}
