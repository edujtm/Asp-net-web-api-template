using Contracts;
using Service.Contracts;

namespace Services
{
    internal sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICustomerService> _customerService;
        private readonly Lazy<IVehicleService> _vehicleService;
        private readonly Lazy<IBookingService> _bookingService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _customerService = new Lazy<ICustomerService>(() => new CustomerService(repositoryManager, logger));
            _vehicleService = new Lazy<IVehicleService>(() => new VehicleService(repositoryManager, logger));
            _bookingService = new Lazy<IBookingService>(() => new BookingService(repositoryManager, logger));
        }

        public ICustomerService CustomerService => _customerService.Value;

        public IVehicleService VehicleService => _vehicleService.Value;

        public IBookingService BookingService => _bookingService.Value;
    }
}
