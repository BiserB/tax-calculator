using Taxation.Domain.Rules.Interfaces;

namespace Taxation.Domain.Rules.Base
{
    public class TaxationContext : ITaxationContext
    {
        public decimal GrossIncome { get; set; }

        public decimal TaxableGrossIncome { get; set; }

        public decimal NetIncome { get; set; }

        public decimal TotalTax { get; set; }

        public decimal CharitySpent { get; set; }
    }
}
