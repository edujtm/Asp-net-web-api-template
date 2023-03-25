

namespace Shared.DTOs
{
    public record CustomerUpdateDto(string Name, DateTime DateBirth, string Address, string Details, IEnumerable<BookingCreationDto>? Bookings);
}
