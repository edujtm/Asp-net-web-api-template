

using Shared.Validators;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public record CustomerUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(BirthDateAttributeValidator), "ValidateBirthDate")]
        public DateTime DateBirth { get; set; }

        [Required]
        [MaxLength(60)]
        public string? Address { get; set; }

        [MaxLength(2000)]
        public string? Details { get; set; }

        // TODO: validation
        public IEnumerable<BookingCreationDto>? Bookings { get; set; }
    }
}
