using Contracts;
using Service.Contracts;

namespace Services
{
    internal sealed class VehicleService : IVehicleService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;

        public VehicleService(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
        }
    }
}
