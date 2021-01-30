using System;
using System.Collections.Generic;

namespace VitoshaBank.Data.Models
{
    public partial class Credits
    {
        public int Id { get; set; }
        public string Iban { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; } 
        public decimal Interest { get; set; } = 7.5M;
        public decimal CreditAmount { get; set; } 
        public decimal Instalment { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now.AddMonths(1);
        public decimal CreditAmountLeft { get; set; } 

        public virtual Users User { get; set; }
    }
}
