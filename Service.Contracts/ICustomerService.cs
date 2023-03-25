using Shared.DTOs;

namespace Service.Contracts
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDto> GetAll(bool trackChanges);
        CustomerDto GetById(Guid Id, bool trackChanges);
        CustomerDto Create(CustomerCreationDto customerCreationDto);
        void DeleteCustomer(Guid Id, bool trackChanges);
    }
}