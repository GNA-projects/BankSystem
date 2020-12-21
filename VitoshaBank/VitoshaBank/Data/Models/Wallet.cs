using System;
using System.Collections.Generic;

#nullable disable

namespace VitoshaBank.Data.Models
{
    public partial class Wallet
    {
        public int Id { get; set; }
        public string Iban { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }

        public virtual User User { get; set; }
    }
}
