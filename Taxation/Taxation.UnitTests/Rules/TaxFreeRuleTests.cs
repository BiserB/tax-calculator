using Taxation.Domain.Constants;
using Taxation.Domain.Rules;
using Taxation.Domain.Rules.Base;

namespace Taxation.UnitTests.Rules
{
    public class TaxFreeRuleTests
    {
        [Fact]
        public void Apply_WithIncomeEqualToOrBelowTaxFreeLimit_NoTaxes()
        {
            // Arrange
            var rule = new TaxFreeRule();
            var grossIncome = DomainConstants.TaxFreeLimit;
            var context = new TaxationContext(grossIncome);

            // Act
            rule.Apply(context);

            // Assert
            Assert.Equal(0, context.TotalTax);
        }

        [Theory]
        [InlineData(2000)]
        [InlineData(3000)]
        [InlineData(10000)]
        public void Apply_WithIncomeExceedingTaxFreeLimit_TaxableGrossIncomeCalcualted(int amountOverLimit)
        {
            // Arrange
            var rule = new TaxFreeRule();
            var context = new TaxationContext(DomainConstants.TaxFreeLimit + amountOverLimit);

            // Act
            rule.Apply(context);

            // Assert
            Assert.Equal(amountOverLimit, context.TaxableGrossIncome);
        }
    }
}
