using Taxation.Common.Models.Config;
using Taxation.Domain.Rules.Base;
using Taxation.Domain.Rules.Interfaces;
using Microsoft.Extensions.Options;
using Taxation.Services.Factories;

namespace Taxation.Services.Features.Calculations
{
    public class TaxCalculator
    {
        private readonly List<ITaxationRule> _rules;
        private readonly TaxSettings _taxSettings;

        public TaxCalculator(IOptions<TaxSettings> configuration)
        {
            _taxSettings = configuration.Value;

            _rules = TaxationRuleFactory.CreateTaxRules(_taxSettings.Rules.OrderBy(c => c.Order).ToList());
        }

        public void AddRule(ITaxationRule rule)
        {
            _rules.Add(rule);
        }

        public ITaxationContext Calculate(decimal grossIncome, decimal charitySpent)
        {
            var context = new TaxationContext(grossIncome, charitySpent);

            foreach (var rule in _rules)
            {
                rule.Apply(context);
            }

            return context;
        }
    }
}
