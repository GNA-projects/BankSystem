using System;
using System.Collections.Generic;

namespace VitoshaBank.Data.Models
{
    public partial class Wallets
    {
        public int Id { get; set; }
        public string Iban { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }

        public virtual Users User { get; set; }
    }
}
