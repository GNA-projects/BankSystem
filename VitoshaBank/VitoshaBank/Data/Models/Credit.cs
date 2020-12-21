using System;
using System.Collections.Generic;

#nullable disable

namespace VitoshaBank.Data.Models
{
    public partial class Credit
    {
        public int Id { get; set; }
        public string Iban { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public decimal Interest { get; set; }
        public decimal Instalment { get; set; }
        public decimal CreditAmount { get; set; }

        public virtual User User { get; set; }
    }
}
