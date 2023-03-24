using Shared.DTOs;

namespace Service.Contracts
{
    public interface IBookingService
    {
        IEnumerable<BookingDto> GetAll(Guid customerId, bool trackChanges);
        BookingDto GetById(Guid customerId, Guid Id, bool trackChanges);
    }
}
