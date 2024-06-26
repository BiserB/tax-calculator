using Taxation.Domain.Rules.Base;
using Taxation.Domain.Rules.Interfaces;

namespace Taxation.Services.Features.Calculations
{
    public class TaxCalculator
    {
        private readonly List<ITaxationRule> _rules = new List<ITaxationRule>();

        public TaxCalculator(List<ITaxationRule> rules)
        {
            _rules = rules;
        }

        public void AddRule(ITaxationRule rule)
        {
            _rules.Add(rule);
        }

        public ITaxationContext Calculate(decimal grossIncome, decimal charitySpent)
        {
            var context = new TaxationContext
            {
                GrossIncome = grossIncome,
                TaxableGrossIncome = grossIncome,
                NetIncome = grossIncome,
                CharitySpent = charitySpent
            };

            foreach (var rule in _rules)
            {
                rule.Apply(context);
            }

            return context;
        }
    }
}
