using System;
using System.Collections.Generic;
using VitoshaBank.Services.GenerateCardInfoService;

namespace VitoshaBank.Data.Models
{
    public partial class Wallets
    {
        private const decimal defaultAmount = 0.00m;
        public int Id { get; set; }
        public string Iban { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; } = defaultAmount;
        public string CardNumber { get; set; } = GenerateCardInfo.GenerateNumber(15);
        public string Cvv { get; set; } = GenerateCardInfo.GenerateCVV(3);
        public DateTime CardExipirationDate { get; set; }

        public virtual Users User { get; set; }
    }
}
