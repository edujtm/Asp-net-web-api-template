
using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public record UserAuthenticationDto
    {
        [Required]
        public string? UserName { get; init; }
        [Required]
        public string? Password { get; init; }
    }

}
