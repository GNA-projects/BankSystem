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
        public decimal Interest { get; set; }
        public decimal Instalment { get; set; }
        public decimal CreditAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal CreditAmountLeft { get; set; }

        public virtual Users User { get; set; }
    }
}
