
using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public record UserCreationDto
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        [Required]
        public string? UserName { get; init; }
        [Required]
        public string? Password { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public ICollection<string>? Roles { get; init; }
    }
}
