using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DTOs;

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
    }
}