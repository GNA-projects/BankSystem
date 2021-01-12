using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VitoshaBank.Services.CalculateDividendService
{
    public static class CalculateDividentService
    {
        public static decimal GetDividentPercent(decimal Amount, int termOfPayment)
        {
            //TODO make validations
            return 0.06M;
        }
    }
}
