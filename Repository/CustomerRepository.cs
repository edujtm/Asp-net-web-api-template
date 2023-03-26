using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Customer>> GetAllAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(Guid Id, bool trackChanges)
        {
            return await FindByCondition(x => x.Id.Equals(Id), trackChanges: trackChanges)
                        .SingleOrDefaultAsync();
        }

        public void CreateCustomer(Customer customer) => Create(customer);

        public void DeleteCustomer(Customer customer)
        {
            Delete(customer);
        }
    }
}
