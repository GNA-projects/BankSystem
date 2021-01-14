using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VitoshaBank.Data.ResponseModels
{
    public class TransactionResponseModel
    {
        public string senderIBAN { get; set; }
        public string reciverIBAN { get; set; }

        public decimal senderAmount { get; set; }
        public decimal reciverAmount { get; set; }
    }
}
