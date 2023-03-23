using Contracts;
using Entities.Models;

namespace Repository
{
    internal class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
