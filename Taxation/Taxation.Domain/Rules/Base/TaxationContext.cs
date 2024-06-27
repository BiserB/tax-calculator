using Taxation.Domain.Rules.Interfaces;

namespace Taxation.Domain.Rules.Base
{
    public class TaxationContext : ITaxationContext
    {
        public TaxationContext(decimal grossIncome, decimal charitySpent = 0m)
        {
            GrossIncome = grossIncome;
            TaxableGrossIncome = grossIncome;
            NetIncome = grossIncome;
            CharitySpent = charitySpent;
        }

        public decimal GrossIncome { get; set; }

        public decimal TaxableGrossIncome { get; set; }

        public decimal NetIncome { get; set; }

        public decimal TotalTax { get; set; }

        public decimal CharitySpent { get; set; }
    }
}
