using Contracts;
using Service.Contracts;

namespace Services
{
    internal sealed class BookingService : IBookingService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public BookingService(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }
    }
}
