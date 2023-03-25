using Shared.DTOs;

namespace Service.Contracts
{
    public interface IBookingService
    {
        IEnumerable<BookingDto> GetAll(Guid customerId, bool trackChanges);
        BookingDto GetById(Guid customerId, Guid Id, bool trackChanges);
        BookingDto Create(Guid customerId, Guid vehicleid, BookingCreationDto bookingCreationDto, bool trackChanges);
        void DeleteBooking(Guid customerId, Guid Id, bool trackChanges);
    }
}
