using System;
using System.Collections.Generic;

#nullable disable

namespace VitoshaBank.Data.Models
{
    public partial class Transaction
    {
        public Transaction()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public int SenderAccountId { get; set; }
        public int RecieverAccountId { get; set; }
        public decimal TransactionAmount { get; set; }
        public DateTime Date { get; set; }

        public virtual BankAccount RecieverAccount { get; set; }
        public virtual BankAccount SenderAccount { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
