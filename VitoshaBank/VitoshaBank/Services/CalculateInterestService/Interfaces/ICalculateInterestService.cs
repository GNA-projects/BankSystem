namespace VitoshaBank.Services.CreditService
{
    public interface ICalculateInterestService
    {
        public const double  interest = 7.5;
        public decimal CalculateCreditAmount(decimal amount);
        public decimal CalculateInstalment(decimal CreditAmount, decimal interest, int period);

    }
}