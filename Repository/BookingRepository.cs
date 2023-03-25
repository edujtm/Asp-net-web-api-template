using Contracts;
using Entities.Models;

namespace Repository
{
    public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
    {
        public BookingRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void Create(Guid CustomerId, Booking booking)
        {
            booking.CustomerId = CustomerId;
            Create(booking);
        }

        public IEnumerable<Booking> GetAll(Guid CustomerId, bool trackChanges)
        {
            return FindByCondition(x => x.CustomerId.Equals(CustomerId), trackChanges: trackChanges)
                .ToList();
        }

        public Booking GetById(Guid CustomerId, Guid Id, bool trackChanges)
        {
            return FindByCondition(x => x.CustomerId.Equals(CustomerId) &&
                    x.Id.Equals(Id), trackChanges: trackChanges)
                .SingleOrDefault();
        }

        public void DeleteBooking(Booking booking)
        {
            Delete(booking);
        }
    }
}
