using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class VehicleRepository : RepositoryBase<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.Model).ToListAsync();
        }

        public async Task<Vehicle> GetByIdAsync(Guid Id, bool trackChanges)
        {
            return await FindByCondition(x => x.Id.Equals(Id), trackChanges: trackChanges).SingleOrDefaultAsync();
        }

        public void CreateVehicle(Vehicle vehicle) => Create(vehicle);
    }
}
