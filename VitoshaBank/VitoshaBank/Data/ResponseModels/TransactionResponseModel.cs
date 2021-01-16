using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VitoshaBank.Data.ResponseModels
{
    public class TransactionResponseModel
    {
        public string senderInfo { get; set; }
        public string reciverInfo { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
