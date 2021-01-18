using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VitoshaBank.Services.CreditService;

namespace VitoshaBank.Services.CalculateInterestService
{
    public class CalculateInterestService : ICalculateInterestService
    {
        double interest = 8.5;
        int period = 12;
        public decimal CalculateCreditAmount(decimal amount)
        {

            double doubleAmount = (double)(amount);
            double coef = 1 + (interest * period) / 100;
            double creditAmount = doubleAmount * coef;
            return (decimal)(creditAmount);

        }
        public decimal CalculateInstalment(decimal CreditAmount, double interest, int period)
        {
            double coef = 1 + interest / 100;
            double a = Math.Pow(coef, period) * (coef - 1);
            double b = Math.Pow(coef, period) - 1;
            double instalment = (double)(CreditAmount) * (a / b);
            return (decimal)(instalment);
        }
    }
}
