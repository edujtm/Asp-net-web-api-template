

using Shared.DTOs;

namespace Service.Contracts
{
    public interface IVehicleService
    {
        IEnumerable<VehicleDto> GetAll(bool trackChanges);
        VehicleDto GetById(Guid Id, bool trackChanges);
        VehicleDto Create(VehicleCreationDto vehicleCreationDto);
    }
}
