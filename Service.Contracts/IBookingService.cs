using Shared.DTOs;

namespace Service.Contracts
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetAllAsync(Guid customerId, bool trackChanges);
        Task<BookingDto> GetByIdAsync(Guid customerId, Guid Id, bool trackChanges);
        Task<BookingDto> CreateAsync(Guid customerId, BookingCreationDto bookingCreationDto, bool trackChanges);
        Task DeleteBookingAsync(Guid customerId, Guid Id, bool trackChanges);
    }
}
