
using Entities.Models;

namespace Contracts
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetAllAsync(bool trackChanges);
        Task<Vehicle> GetByIdAsync(Guid Id, bool trackChanges);
        void Create(Vehicle vehicle);
    }
}
