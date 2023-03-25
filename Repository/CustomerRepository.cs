using Contracts;
using Entities.Models;

namespace Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Customer> GetAll(bool trackChanges)
        {
            return FindAll(trackChanges).OrderBy(c => c.Name).ToList();
        }

        public Customer GetById(Guid Id, bool trackChanges)
        {
            return FindByCondition(x => x.Id.Equals(Id), trackChanges: trackChanges).SingleOrDefault();
        }

        public void CreateCustomer(Customer customer) => Create(customer);

        public void DeleteCustomer(Customer customer)
        {
            Delete(customer);
        }
    }
}
