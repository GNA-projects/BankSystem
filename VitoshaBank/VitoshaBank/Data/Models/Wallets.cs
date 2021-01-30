using System;
using System.Collections.Generic;
using VitoshaBank.Services.GenerateCardInfoService;

namespace VitoshaBank.Data.Models
{
    public partial class Wallets
    {
        public int Id { get; set; }
        public string Iban { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; } 
        public string CardNumber { get; set; } = GenerateCardInfo.GenerateNumber(15);
        public string Cvv { get; set; } = GenerateCardInfo.GenerateCVV(3);
        public DateTime CardExipirationDate { get; set; } = DateTime.Now.AddMonths(60);

        public virtual Users User { get; set; }
    }
}
