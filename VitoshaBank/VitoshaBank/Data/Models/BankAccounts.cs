using System;
using System.Collections.Generic;

namespace VitoshaBank.Data.Models
{
    public partial class BankAccounts
    {
        public BankAccounts()
        {
            TransactionsRecieverAccount = new HashSet<Transactions>();
            TransactionsSenderAccount = new HashSet<Transactions>();
        }

        public int Id { get; set; }
        public string Iban { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }

        public virtual Users User { get; set; }
        public virtual Cards Cards { get; set; }
        public virtual ICollection<Transactions> TransactionsRecieverAccount { get; set; }
        public virtual ICollection<Transactions> TransactionsSenderAccount { get; set; }
    }
}
