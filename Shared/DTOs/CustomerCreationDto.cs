

using System.ComponentModel.DataAnnotations;
using Entities.Enums;
using Shared.Validators;

namespace Shared.DTOs
{
    public record CustomerCreationDto
    {
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(BirthDateAttributeValidator), "ValidateBirthDate")]
        public DateTime DateBirth { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Gender is required and it can't be lower than 1")]
        [EnumDataType(typeof(GenderEnum))]
        public int Gender { get; set; }

        [Required]
        [MaxLength(60)]
        public string? Address { get; set; }

        [MaxLength(2000)]
        public string? Details { get; set; }
    }
}
