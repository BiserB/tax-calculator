using Taxation.Domain.Constants;
using Taxation.Domain.Rules.Base;
using Taxation.Domain.Rules.Interfaces;

namespace Taxation.Domain.Rules
{
    public class SocialContributionRule : TaxationRule
    {        
        private const decimal MaxTaxableLimit = 3000;
        private const decimal ContributionRate = 0.15m;

        public decimal GetMaxTaxableLimit() => MaxTaxableLimit;
        public decimal GetContributionRate() => ContributionRate;
        public decimal GetMaxTaxableIncome() => MaxTaxableLimit - DomainConstants.TaxFreeLimit;

        public override void Apply(ITaxationContext context)
        {
            if (context.TaxableGrossIncome > DomainConstants.TaxFreeLimit)
            {
                var taxableIncome = Math.Min(context.TaxableGrossIncome, GetMaxTaxableIncome());
                
                var contribution = taxableIncome * ContributionRate;

                context.TotalTax += contribution;

                context.NetIncome -= contribution;
            }
        }
    }
}
