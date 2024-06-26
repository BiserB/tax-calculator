using Taxation.Common.Models.Config;
using Taxation.Domain.Rules;
using Taxation.Domain.Rules.Interfaces;

namespace Taxation.Services.Factories
{
    public static class TaxationRuleFactory
    {
        public static List<ITaxationRule> CreateTaxRules(List<TaxRuleConfiguration> configurations)
        {
            var rules = new List<ITaxationRule>();

            foreach (var config in configurations)
            {
                switch (config.RuleType)
                {
                    case TaxRuleType.TaxFree:
                        rules.Add(new TaxFreeRule());
                        break;
                    case TaxRuleType.IncomeTax:
                        rules.Add(new IncomeTaxRule());
                        break;
                    case TaxRuleType.SocialContribution:
                        rules.Add(new SocialContributionRule());
                        break;
                    case TaxRuleType.CharityDeduction:
                        rules.Add(new CharityDeductionRule());
                        break;
                }
            }

            return rules;
        }
    }
}
