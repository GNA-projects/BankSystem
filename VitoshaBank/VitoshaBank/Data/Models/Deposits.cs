using System;
using System.Collections.Generic;

namespace VitoshaBank.Data.Models
{
    public partial class Deposits
    {
        public int Id { get; }
        public string Iban { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public decimal Divident { get; set; }
        public DateTime PaymentDate { get; set; }
        public int TermOfPayment { get; set; }

        public virtual Users User { get; set; }
    }
}
