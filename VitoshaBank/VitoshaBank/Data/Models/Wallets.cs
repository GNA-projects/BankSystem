using System;
using System.Collections.Generic;

namespace VitoshaBank.Data.Models
{
    public partial class Wallets
    {
        public int Id { get; }
        public string Iban { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string CardNumber { get; set; }
        public string Cvv { get; set; }
        public DateTime CardExipirationDate { get; set; }

        public virtual Users User { get; set; }
    }
}
