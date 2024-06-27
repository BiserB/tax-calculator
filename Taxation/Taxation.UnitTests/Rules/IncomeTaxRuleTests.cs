using Taxation.Domain.Constants;
using Taxation.Domain.Rules;
using Taxation.Domain.Rules.Base;

namespace Taxation.UnitTests.Rules
{
    public class IncomeTaxRuleTests
    {
        [Fact]
        public void Apply_WithIncomeEqualToOrBelowTaxFreeLimit_ReturnsNoTax()
        {
            // Arrange
            var rule = new IncomeTaxRule();
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
            var rule = new IncomeTaxRule();
            var context = new TaxationContext(grossIncome);

            // Act
            rule.Apply(context);

            // Assert
            Assert.Equal(grossIncome * rule.GetTaxRate(), context.TotalTax);
            Assert.Equal(grossIncome - context.TotalTax, context.NetIncome);
        }
    }
}