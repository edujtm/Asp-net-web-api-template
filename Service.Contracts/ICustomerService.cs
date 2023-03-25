using Shared.DTOs;
using Entities.Models;

namespace Service.Contracts
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDto> GetAll(bool trackChanges);
        CustomerDto GetById(Guid Id, bool trackChanges);
        CustomerDto Create(CustomerCreationDto customerCreationDto);
        void DeleteCustomer(Guid Id, bool trackChanges);
        void UpdateCustomer(Guid Id, CustomerUpdateDto customer, bool trackChanges);
        (CustomerUpdateDto customerPatchDto, Customer customer) GetCustomerForPatch(Guid id, bool trackChanges);
        void Patch(CustomerUpdateDto customerPatchDto, Customer customer);
    }
}