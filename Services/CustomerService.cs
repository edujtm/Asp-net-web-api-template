using AutoMapper;
using Contracts;
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
            try
            {
                var customers = _repository.CustomerRepository.GetAll(trackChanges);
                return _mapper.Map<IEnumerable<CustomerDto>>(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(CustomerService)} > {nameof(GetAll)} service method {ex}.");
                throw;
            }
        }
    }
}