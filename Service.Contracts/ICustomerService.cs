using Shared.DTOs;
using Entities.Models;
using Shared.RequestHelper;

namespace Service.Contracts
{
    public interface ICustomerService
    {
        Task<(IEnumerable<CustomerDto> customerDtos, MetaDataRequest metaDataRequest)> GetAllAsync(CustomerParams customerParams, bool trackChanges);
        Task<CustomerDto> GetByIdAsync(Guid Id, bool trackChanges);
        Task<CustomerDto> CreateAsync(CustomerCreationDto customerCreationDto);
        Task DeleteCustomerAsync(Guid Id, bool trackChanges);
        Task UpdateCustomerAsync(Guid Id, CustomerUpdateDto customer, bool trackChanges);
        Task<(CustomerUpdateDto customerPatchDto, Customer customer)> GetCustomerForPatchAsync(Guid id, bool trackChanges);
        Task PatchAsync(CustomerUpdateDto customerPatchDto, Customer customer);
    }
}