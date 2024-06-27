using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Taxation.API.Validations
{
    public class FullNameValidation : ValidationAttribute
    {
        public const string NameFormat = @"^[a-zA-Z\s]+$";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var fullName = value as string;

            if (string.IsNullOrEmpty(fullName))
            {
                return new ValidationResult(Message.FullNameRequired);
            }

            var words = fullName.Split(' ');

            if (words.Length < 2)
            {
                return new ValidationResult(Message.AtLeastTwoWords);
            }

            if (!Regex.IsMatch(fullName, NameFormat))
            {
                return new ValidationResult(Message.OnlyLetters);
            }

            return ValidationResult.Success;
        }
    }
}
