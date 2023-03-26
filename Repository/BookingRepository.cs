using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Booking>> GetAllAsync(Guid CustomerId, bool trackChanges)
        {
            return await FindByCondition(x => x.CustomerId.Equals(CustomerId), trackChanges: trackChanges)
                .ToListAsync();
        }

        public async Task<Booking> GetByIdAsync(Guid CustomerId, Guid Id, bool trackChanges)
        {
            return await FindByCondition(x => x.CustomerId.Equals(CustomerId) &&
                    x.Id.Equals(Id), trackChanges: trackChanges)
                .SingleOrDefaultAsync();
        }

        public void DeleteBooking(Booking booking)
        {
            Delete(booking);
        }
    }
}
