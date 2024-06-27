using Taxation.Domain.Rules.Base;
using Taxation.Domain.Rules.Interfaces;

namespace Taxation.Domain.Rules
{
    public class CharityDeductionRule : TaxationRule
    {
        private const decimal MaxCharityRate = 0.10m;

        public decimal GetMaxCharityRate() => MaxCharityRate;

        public override void Apply(ITaxationContext context)
        {
            var maxCharityAmount = context.TaxableGrossIncome * MaxCharityRate;

            var charityDeduction = Math.Min(context.CharitySpent, maxCharityAmount);

            context.TaxableGrossIncome -= charityDeduction;
        }
    }
}
