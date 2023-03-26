using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTOs;
using System.ComponentModel.Design;

namespace Services
{
    internal sealed class CustomerService : ICustomerService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CustomerService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CustomerDto> CreateAsync(CustomerCreationDto customerCreationDto)
        {
            var customer = _mapper.Map<Customer>(customerCreationDto);

            _repository.CustomerRepository.Create(customer);
            await _repository.SaveAsync();

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task DeleteCustomerAsync(Guid Id, bool trackChanges)
        {
            var customer = await _repository.CustomerRepository.GetByIdAsync(Id, trackChanges);
            if (customer == null) { throw new ResourceNotFoundException(Id); }

            _repository.CustomerRepository.DeleteCustomer(customer);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync(bool trackChanges)
        {
            var customers = await _repository.CustomerRepository.GetAllAsync(trackChanges);
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> GetByIdAsync(Guid Id, bool trackChanges)
        {
            var customer = await _repository.CustomerRepository.GetByIdAsync(Id, trackChanges);
            if (customer == null) { throw new ResourceNotFoundException(Id); }
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<(CustomerUpdateDto customerPatchDto, Customer customer)> GetCustomerForPatchAsync(Guid id, bool trackChanges)
        {
            var customer = await _repository.CustomerRepository.GetByIdAsync(id, trackChanges);
            if (customer == null) { throw new ResourceNotFoundException(id); }

            var customerToPatch = _mapper.Map<CustomerUpdateDto>(customer);
            return (customerToPatch, customer);
        }

        public async Task PatchAsync(CustomerUpdateDto customerPatchDto, Customer customer)
        {
            _mapper.Map(customerPatchDto, customer);
            await _repository.SaveAsync();
        }

        public async Task UpdateCustomerAsync(Guid Id, CustomerUpdateDto customerDto, bool trackChanges)
        {
            var customerEntity = await _repository.CustomerRepository.GetByIdAsync(Id, trackChanges);
            if (customerEntity is null)
                throw new ResourceNotFoundException(Id);

            _mapper.Map(customerDto, customerEntity);
            await _repository.SaveAsync();
        }
    }
}