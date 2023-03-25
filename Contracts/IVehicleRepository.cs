
using Entities.Models;

namespace Contracts
{
    public interface IVehicleRepository
    {
        IEnumerable<Vehicle> GetAll(bool trackChanges);
        Vehicle GetById(Guid Id, bool trackChanges);
        void Create(Vehicle vehicle);
    }
}
