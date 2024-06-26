using Taxation.Domain.Constants;
using Taxation.Domain.Rules.Base;
using Taxation.Domain.Rules.Interfaces;

namespace Taxation.Domain.Rules
{
    public class SocialContributionRule : TaxationRule
    {        
        private const decimal MaxTaxableIncome = 3000;
        private const decimal ContributionRate = 0.15m;

        public decimal GetMaxTaxableIncome() => MaxTaxableIncome;
        public decimal GetContributionRate() => ContributionRate;

        public override void Apply(ITaxationContext context)
        {
            if (context.TaxableGrossIncome > DomainConstants.TaxFreeLimit)
            {
                var taxableIncome = Math.Min(context.TaxableGrossIncome, MaxTaxableIncome - DomainConstants.TaxFreeLimit);
                
                var contribution = taxableIncome * ContributionRate;

                context.TotalTax += contribution;

                context.NetIncome -= contribution;
            }
        }
    }
}
