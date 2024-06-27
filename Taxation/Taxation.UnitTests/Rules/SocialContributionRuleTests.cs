using Taxation.Domain.Constants;
using Taxation.Domain.Rules.Base;
using Taxation.Domain.Rules;

namespace Taxation.UnitTests.Rules
{
    public class SocialContributionRuleTests
    {
        [Fact]
        public void Apply_WithIncomeEqualToOrBelowTaxFreeLimit_ReturnsNoTax()
        {
            // Arrange
            var rule = new SocialContributionRule();
            var grossIncome = DomainConstants.TaxFreeLimit;
            var context = new TaxationContext(grossIncome);

            // Act
            rule.Apply(context);

            // Assert
            Assert.Equal(0, context.TotalTax);
            Assert.Equal(grossIncome, context.NetIncome);
        }

        [Theory]
        [InlineData(3000)]
        [InlineData(5000)]
        [InlineData(10000)]
        public void Apply_WithIncomeExceedingTaxFreeLimit_ReturnsCorrectTax(decimal grossIncome)
        {
            // Arrange
            var rule = new SocialContributionRule();
            var context = new TaxationContext(grossIncome);

            // Act
            rule.Apply(context);

            // Assert
            var contribution = Math.Min(context.TaxableGrossIncome, rule.GetMaxTaxableIncome()) * rule.GetContributionRate();
            Assert.Equal(contribution, context.TotalTax);
            Assert.Equal(grossIncome - contribution, context.NetIncome);
        }
    }
}
