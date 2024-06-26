namespace Taxation.Domain.Rules.Interfaces
{
    public interface ITaxationRule
    {
        void Apply(ITaxationContext context);
    }
}
