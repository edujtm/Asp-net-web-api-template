using Entities.Models;

namespace Contracts
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll(bool trackChanges);
        Customer GetById(Guid Id, bool trackChanges);
        void Create(Customer customer);
        void DeleteCustomer(Customer customer);
    }
}
