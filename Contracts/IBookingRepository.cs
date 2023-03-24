
using Entities.Models;

namespace Contracts
{
    public interface IBookingRepository
    {
        IEnumerable<Booking> GetAll(Guid CustomerId, bool trackChanges);
        Booking GetById(Guid CustomerId, Guid Id, bool trackChanges);
    }
}
