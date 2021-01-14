using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VitoshaBank.Services.CalculateDividendService.Interfaces
{
    public interface ICalculateDividentService
    {
        public decimal GetDividentPercent(decimal Amount, int termOfPayment);
    }
}
