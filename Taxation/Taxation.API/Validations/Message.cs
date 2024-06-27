namespace Taxation.API.Validations
{
    public static class Message
    {
        public const string InvalidGrossIncome = "GrossIncome must be a valid positive number.";
        
        public const string FullNameRequired = "FullName is required.";

        public const string AtLeastTwoWords = "FullName must contain at least two words.";

        public const string OnlyLetters = "FullName can only contain letters and spaces.";

        public const string InvalidSSN = $"SSN must be a valid 5 to 10 digit number.";

        public const string InvalidCharity = "CharitySpent must be a valid positive number.";
    }
}
