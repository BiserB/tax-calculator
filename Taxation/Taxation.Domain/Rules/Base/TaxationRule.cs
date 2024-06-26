using Taxation.Domain.Rules.Interfaces;

namespace Taxation.Domain.Rules.Base
{
    public abstract class TaxationRule : ITaxationRule
    {
        public abstract void Apply(ITaxationContext context);
    }
}
