using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICustomerService> _customerService;
        private readonly Lazy<IVehicleService> _vehicleService;
        private readonly Lazy<IBookingService> _bookingService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(
            IRepositoryManager repositoryManager, 
            ILoggerManager logger, 
            IMapper mapper, 
            UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _customerService = new Lazy<ICustomerService>(() => new CustomerService(repositoryManager, logger, mapper));
            _vehicleService = new Lazy<IVehicleService>(() => new VehicleService(repositoryManager, logger, mapper));
            _bookingService = new Lazy<IBookingService>(() => new BookingService(repositoryManager, logger, mapper));
            _authenticationService = 
                new Lazy<IAuthenticationService>(() => new AuthenticationService(logger, mapper, userManager, roleManager, configuration));
        }

        public ICustomerService CustomerService => _customerService.Value;

        public IVehicleService VehicleService => _vehicleService.Value;

        public IBookingService BookingService => _bookingService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;

    }
}
