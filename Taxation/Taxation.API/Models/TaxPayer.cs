using System.ComponentModel.DataAnnotations;
using Taxation.API.Validations;
using Taxation.Domain.Constants;

namespace Taxation.API.Models
{
    public class TaxPayer
    {
        [Required]
        [FullNameValidation]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = Message.InvalidGrossIncome)]
        public decimal GrossIncome { get; set; }

        [Required]
        [RegularExpression(DomainConstants.SSNumberFormat, ErrorMessage = Message.InvalidSSN)]
        public string SSN { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = Message.InvalidCharity)]
        public decimal? CharitySpent { get; set; }
    }
}
