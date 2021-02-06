using System;
using System.Collections.Generic;

namespace VitoshaBank.Data.Models
{
    public partial class ChargeAccounts
    {
        public int Id { get; }
        public string Iban { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }

        public virtual Users User { get; set; }
        public virtual Cards Cards { get; set; }
    }
}
