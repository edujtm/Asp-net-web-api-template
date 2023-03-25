using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTOs;

namespace Services
{
    internal sealed class VehicleService : IVehicleService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public VehicleService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        public VehicleDto Create(VehicleCreationDto vehicleCreationDto)
        {
            var vehicle = _mapper.Map<Vehicle>(vehicleCreationDto);

            _repositoryManager.VehicleRepository.Create(vehicle);
            _repositoryManager.Save();

            return _mapper.Map<VehicleDto>(vehicle);
        }

        public IEnumerable<VehicleDto> GetAll(bool trackChanges)
        {
            var vehicles = _repositoryManager.VehicleRepository.GetAll(trackChanges);
            return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
        }

        public VehicleDto GetById(Guid Id, bool trackChanges)
        {
            var vehicle = _repositoryManager.VehicleRepository.GetById(Id, trackChanges);
            if (vehicle is null) { throw new ResourceNotFoundException(Id); }
            return _mapper.Map<VehicleDto>(vehicle);
        }
    }
}
