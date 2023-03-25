using Contracts;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;

        private readonly Lazy<ICustomerRepository> _customerRepository;
        private readonly Lazy<IVehicleRepository> _vehicleRepository;
        private readonly Lazy<IBookingRepository> _bookingRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;

            _customerRepository = new Lazy<ICustomerRepository>(() =>
                new CustomerRepository(repositoryContext));
            _vehicleRepository = new Lazy<IVehicleRepository>(() =>
                new VehicleRepository(repositoryContext));
            _bookingRepository = new Lazy<IBookingRepository>(() =>
                new BookingRepository(repositoryContext));
        }

        public ICustomerRepository CustomerRepository => _customerRepository.Value;

        public IVehicleRepository VehicleRepository => _vehicleRepository.Value;

        public IBookingRepository BookingRepository => _bookingRepository.Value;

        public void Save()
        {
            _repositoryContext.SaveChanges();
        }
    }
}
