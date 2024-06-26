using Taxation.Domain.Constants;
using Taxation.Domain.Rules.Base;
using Taxation.Domain.Rules.Interfaces;

namespace Taxation.Domain.Rules
{
    public class IncomeTaxRule : TaxationRule
    {
        private const decimal TaxRate = 0.10m;

        public decimal GetTaxRate() => TaxRate;

        public override void Apply(ITaxationContext context)
        {
            if (context.TaxableGrossIncome > DomainConstants.TaxFreeLimit)
            {
                var tax = context.TaxableGrossIncome * TaxRate;

                context.TotalTax += tax;

                context.NetIncome -= tax;
            }
        }
    }
}
