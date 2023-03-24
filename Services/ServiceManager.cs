using AutoMapper;
using Contracts;
using Service.Contracts;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICustomerService> _customerService;
        private readonly Lazy<IVehicleService> _vehicleService;
        private readonly Lazy<IBookingService> _bookingService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _customerService = new Lazy<ICustomerService>(() => new CustomerService(repositoryManager, logger, mapper));
            _vehicleService = new Lazy<IVehicleService>(() => new VehicleService(repositoryManager, logger, mapper));
            _bookingService = new Lazy<IBookingService>(() => new BookingService(repositoryManager, logger, mapper));
        }

        public ICustomerService CustomerService => _customerService.Value;

        public IVehicleService VehicleService => _vehicleService.Value;

        public IBookingService BookingService => _bookingService.Value;
    }
}
