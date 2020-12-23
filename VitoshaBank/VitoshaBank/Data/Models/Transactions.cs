using System;
using System.Collections.Generic;

namespace VitoshaBank.Data.Models
{
    public partial class Transactions
    {
        public int Id { get; set; }
        public int SenderAccountId { get; set; }
        public int RecieverAccountId { get; set; }
        public decimal TransactionAmount { get; set; }
        public DateTime Date { get; set; }

        public virtual BankAccounts RecieverAccount { get; set; }
        public virtual BankAccounts SenderAccount { get; set; }
    }
}
