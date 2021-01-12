using System;
using System.Collections.Generic;
using VitoshaBank.Services.IBANGeneratorService;

namespace VitoshaBank.Data.Models
{
    public partial class Wallets
    {
        private const decimal defaultValue = 0.0M;
        public int Id { get; }
        public string Iban { get; set; } 
        public int UserId { get; set; }
        public decimal Amount { get; set; } = defaultValue;

        public virtual Users User { get; }
    }
}
