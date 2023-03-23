using Contracts;
using Entities.Models;

namespace Repository
{
    public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
    {
        public BookingRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
