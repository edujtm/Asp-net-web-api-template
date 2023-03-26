

using Shared.DTOs;

namespace Service.Contracts
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleDto>> GetAllAsync(bool trackChanges);
        Task<VehicleDto> GetByIdAsync(Guid Id, bool trackChanges);
        Task<VehicleDto> CreateAsync(VehicleCreationDto vehicleCreationDto);
    }
}
