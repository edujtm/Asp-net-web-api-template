
using Entities.Models;

namespace Contracts
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllAsync(Guid CustomerId, bool trackChanges);
        Task<Booking> GetByIdAsync(Guid CustomerId, Guid Id, bool trackChanges);
        void Create(Guid CustomerId, Booking booking);
        void DeleteBooking(Booking booking);
    }
}
