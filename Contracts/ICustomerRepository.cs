using Entities.Models;
using Shared.RequestHelper;

namespace Contracts
{
    public interface ICustomerRepository
    {
        Task<PagedList<Customer>> GetAllAsync(CustomerParams customerParams, bool trackChanges);
        Task<Customer> GetByIdAsync(Guid Id, bool trackChanges);
        void Create(Customer customer);
        void DeleteCustomer(Customer customer);
    }
}
