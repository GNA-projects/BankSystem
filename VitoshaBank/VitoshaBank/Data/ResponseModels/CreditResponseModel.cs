using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VitoshaBank.Data.ResponseModels
{
    public class CreditResponseModel
    {
        public string IBAN { get; set; }
        public decimal Amount { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal Instalment { get; set; }
    }
}
