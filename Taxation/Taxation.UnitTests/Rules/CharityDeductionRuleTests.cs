using Taxation.Domain.Constants;
using Taxation.Domain.Rules.Base;
using Taxation.Domain.Rules;

namespace Taxation.UnitTests.Rules
{
    public class CharityDeductionRuleTests
    {
        [Fact]
        public void Apply_WithZeroCharity_NoDeduction()
        {
            // Arrange
            var rule = new CharityDeductionRule();
            var grossIncome = DomainConstants.TaxFreeLimit;
            var context = new TaxationContext(grossIncome);

            // Act
            rule.Apply(context);

            // Assert
            Assert.Equal(grossIncome, context.TaxableGrossIncome);
        }

        [Theory]
        [InlineData(3000, 150)]
        [InlineData(5000, 455)]
        [InlineData(10000, 5000)]
        public void Apply_WithCharity_DeductionApplied(decimal grossIncome, decimal charity)
        {
            // Arrange
            var rule = new CharityDeductionRule();
            var context = new TaxationContext(grossIncome, charity);

            // Act
            rule.Apply(context);


            // Assert
            var charityDeducted = Math.Min(context.CharitySpent, context.TaxableGrossIncome * rule.GetMaxCharityRate());
            Assert.Equal(grossIncome - charityDeducted, context.TaxableGrossIncome);
        }
    }
}
