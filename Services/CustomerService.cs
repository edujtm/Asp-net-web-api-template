using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTOs;
using Shared.RequestHelper;

namespace Services
{
    internal sealed class CustomerService : ICustomerService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CustomerService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repositoryManager = repository;
            _logger = logger;
            _mapper = mapper;
        }

        private async Task<Customer> GetCustomerByIdAndCheckExistence(Guid customerId, bool trackChanges)
        {
            var customer = await _repositoryManager.CustomerRepository.GetByIdAsync(customerId, trackChanges);
            return (customer == null) ? throw new ResourceNotFoundException(customerId) : customer;
        }

        public async Task<CustomerDto> CreateAsync(CustomerCreationDto customerCreationDto)
        {
            var customer = _mapper.Map<Customer>(customerCreationDto);

            _repositoryManager.CustomerRepository.Create(customer);
            await _repositoryManager.SaveAsync();

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task DeleteCustomerAsync(Guid Id, bool trackChanges)
        {
            var customer = await GetCustomerByIdAndCheckExistence(Id, trackChanges);

            _repositoryManager.CustomerRepository.DeleteCustomer(customer);
            await _repositoryManager.SaveAsync();
        }

        public async Task<(IEnumerable<CustomerDto> customerDtos, MetaDataRequest metaDataRequest)> GetAllAsync(CustomerParams customerParams, bool trackChanges)
        {
            var customersWithMetadata = await _repositoryManager.CustomerRepository.GetAllAsync(customerParams, trackChanges);
            var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customersWithMetadata);

            return (customerDtos: customersDto, metaDataRequest: customersWithMetadata.MetaDataRequest);
        }

        public async Task<CustomerDto> GetByIdAsync(Guid Id, bool trackChanges)
        {
            var customer = await GetCustomerByIdAndCheckExistence(Id, trackChanges);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<(CustomerUpdateDto customerPatchDto, Customer customer)> GetCustomerForPatchAsync(Guid Id, bool trackChanges)
        {
            var customer = await GetCustomerByIdAndCheckExistence(Id, trackChanges);

            var customerToPatch = _mapper.Map<CustomerUpdateDto>(customer);
            return (customerToPatch, customer);
        }

        public async Task PatchAsync(CustomerUpdateDto customerPatchDto, Customer customer)
        {
            _mapper.Map(customerPatchDto, customer);
            await _repositoryManager.SaveAsync();
        }

        public async Task UpdateCustomerAsync(Guid Id, CustomerUpdateDto customerDto, bool trackChanges)
        {
            var customer = await GetCustomerByIdAndCheckExistence(Id, trackChanges);

            _mapper.Map(customerDto, customer);
            await _repositoryManager.SaveAsync();
        }
    }
}