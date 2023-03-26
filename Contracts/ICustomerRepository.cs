using Entities.Models;

namespace Contracts
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync(bool trackChanges);
        Task<Customer> GetByIdAsync(Guid Id, bool trackChanges);
        void Create(Customer customer);
        void DeleteCustomer(Customer customer);
    }
}
