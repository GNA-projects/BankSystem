using System;
using System.Collections.Generic;

namespace VitoshaBank.Data.Models
{
    public partial class Transactions
    {
        public Transactions()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string SenderAccountInfo { get; set; }
        public string RecieverAccountInfo { get; set; }
        public decimal TransactionAmount { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
