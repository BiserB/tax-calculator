using System.ComponentModel.DataAnnotations;

namespace Taxation.Common.Models.Input
{
    public class TaxPayer
    {
        [Required]
        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public decimal GrossIncome { get; set; }

        [Required]
        public string SSN { get; set; }

        public decimal CharitySpent { get; set; }
    }
}
