using System;
using System.Collections.Generic;

namespace VitoshaBank.Data.Models
{
    public partial class Cards
    {
        public int Id { get; }
        public int UserId { get; set; }
        public string CardNumber { get; set; } 
        public string Cvv { get; set; }
        public int ChargeAccountId { get; set; }
        public DateTime CardExiprationDate { get; set; }

        public virtual ChargeAccounts ChargeAccount { get; set; }
        public virtual Users User { get; set; }
    }
}
