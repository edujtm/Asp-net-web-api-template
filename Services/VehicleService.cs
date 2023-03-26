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

        public async Task<VehicleDto> CreateAsync(VehicleCreationDto vehicleCreationDto)
        {
            var vehicle = _mapper.Map<Vehicle>(vehicleCreationDto);

            _repositoryManager.VehicleRepository.Create(vehicle);
            await _repositoryManager.SaveAsync();

            return _mapper.Map<VehicleDto>(vehicle);
        }

        public async Task<IEnumerable<VehicleDto>> GetAllAsync(bool trackChanges)
        {
            var vehicles = await _repositoryManager.VehicleRepository.GetAllAsync(trackChanges);
            return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
        }

        public async Task<VehicleDto> GetByIdAsync(Guid Id, bool trackChanges)
        {
            var vehicle = await _repositoryManager.VehicleRepository.GetByIdAsync(Id, trackChanges);
            if (vehicle is null) { throw new ResourceNotFoundException(Id); }
            return _mapper.Map<VehicleDto>(vehicle);
        }
    }
}
