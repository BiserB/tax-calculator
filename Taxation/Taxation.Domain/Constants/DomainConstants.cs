namespace Taxation.Domain.Constants
{
    public static class DomainConstants
    {
        public const decimal TaxFreeLimit = 1000;

        public const int SSNumberMinLength = 5;

        public const int SSNumberMaxLength = 10;

        public const string SSNumberFormat = @"^\d{5,10}$";
    }
}
