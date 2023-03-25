using Contracts;
using Entities.Models;

namespace Repository
{
    public class VehicleRepository : RepositoryBase<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Vehicle> GetAll(bool trackChanges)
        {
            return FindAll(trackChanges).OrderBy(c => c.Model).ToList();
        }

        public Vehicle GetById(Guid Id, bool trackChanges)
        {
            return FindByCondition(x => x.Id.Equals(Id), trackChanges: trackChanges).SingleOrDefault();
        }

        public void CreateVehicle(Vehicle vehicle) => Create(vehicle);
    }
}
