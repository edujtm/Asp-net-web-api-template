
namespace Contracts
{
    public interface IRepositoryManager
    {
        ICustomerRepository CustomerRepository { get; }
        IVehicleRepository VehicleRepository { get; }   
        IBookingRepository BookingRepository { get; }   
        Task SaveAsync();
    }
}
