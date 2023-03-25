
using System.ComponentModel.DataAnnotations;

namespace Shared.Validators
{
    public class BirthDateAttributeValidator
    {
        public static ValidationResult ValidateBirthDate(DateTime? birthDate, ValidationContext validationContext)
        {
            if (birthDate == DateTime.MinValue) return new ValidationResult("Birth date is required.");

            if (birthDate > DateTime.Today) return new ValidationResult("Birth date must be previous than today's date.");

            return ValidationResult.Success;
        }
    }
}
