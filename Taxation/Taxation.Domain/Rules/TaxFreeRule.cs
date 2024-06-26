using Taxation.Domain.Rules.Base;
using Taxation.Domain.Rules.Interfaces;

namespace Taxation.Domain.Rules
{
    public class TaxFreeRule : TaxationRule
    {
        private const decimal TaxFreeLimit = 1000;

        public override void Apply(ITaxationContext context)
        {
            if (context.GrossIncome > TaxFreeLimit)
            {
                context.TaxableGrossIncome = context.GrossIncome - TaxFreeLimit;
            }
        }
    }
}
