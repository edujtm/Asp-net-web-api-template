
namespace Service.Contracts
{
    public interface IServiceManager
    {
        ICustomerService CustomerService { get; }   
        IVehicleService VehicleService { get; } 
        IBookingService BookingService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}
