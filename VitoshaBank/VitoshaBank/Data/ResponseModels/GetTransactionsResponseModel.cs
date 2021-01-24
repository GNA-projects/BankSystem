using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VitoshaBank.Data.ResponseModels
{
    public class GetTransactionsResponseModel
    {
        public string SenderInfo { get; set; }
        public string ReciverInfo { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
