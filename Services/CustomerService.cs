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

        public CustomerDto Create(CustomerCreationDto customerCreationDto)
        {
            var customer = _mapper.Map<Customer>(customerCreationDto);

            _repository.CustomerRepository.Create(customer);
            _repository.Save();

            return _mapper.Map<CustomerDto>(customer);
        }

        public void DeleteCustomer(Guid Id, bool trackChanges)
        {
            var customer = _repository.CustomerRepository.GetById(Id, trackChanges);
            if (customer == null) { throw new ResourceNotFoundException(Id); }

            _repository.CustomerRepository.DeleteCustomer(customer);
            _repository.Save();
        }

        public IEnumerable<CustomerDto> GetAll(bool trackChanges)
        {
            var customers = _repository.CustomerRepository.GetAll(trackChanges);
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public CustomerDto GetById(Guid Id, bool trackChanges)
        {
            var customer = _repository.CustomerRepository.GetById(Id, trackChanges);
            if (customer == null) { throw new ResourceNotFoundException(Id); }
            return _mapper.Map<CustomerDto>(customer);
        }

        public (CustomerUpdateDto customerPatchDto, Customer customer) GetCustomerForPatch(Guid id, bool trackChanges)
        {
            var customer = _repository.CustomerRepository.GetById(id, trackChanges);
            if (customer == null) { throw new ResourceNotFoundException(id); }

            var customerToPatch = _mapper.Map<CustomerUpdateDto>(customer);
            return (customerToPatch, customer);
        }

        public void Patch(CustomerUpdateDto customerPatchDto, Customer customer)
        {
            _mapper.Map(customerPatchDto, customer);
            _repository.Save();
        }

        public void UpdateCustomer(Guid Id, CustomerUpdateDto customerDto, bool trackChanges)
        {
            var customerEntity = _repository.CustomerRepository.GetById(Id, trackChanges);
            if (customerEntity is null)
                throw new ResourceNotFoundException(Id);

            _mapper.Map(customerDto, customerEntity);
            _repository.Save();
        }
    }
}