using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestHelper;

namespace Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<Customer>> GetAllAsync(CustomerParams customerParams, bool trackChanges)
        {
            var customers = await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();

            return PagedList<Customer>.ToPagedList(customers, customerParams.PageNumber, customerParams.PageSize);
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
