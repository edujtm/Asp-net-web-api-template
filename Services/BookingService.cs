using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DTOs;

namespace Services
{
    internal sealed class BookingService : IBookingService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public BookingService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<BookingDto> GetAll(Guid customerId, bool trackChanges)
        {
            var customer = _repositoryManager.CustomerRepository.GetById(customerId, trackChanges);
            if (customer == null) { throw new ResourceNotFoundException(customerId); }

            var bookings = _repositoryManager.BookingRepository.GetAll(customerId, trackChanges);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public BookingDto GetById(Guid customerId, Guid Id, bool trackChanges)
        {
            var customer = _repositoryManager.CustomerRepository.GetById(customerId, trackChanges);
            if (customer == null) { throw new ResourceNotFoundException(customerId); }

            var booking = _repositoryManager.BookingRepository.GetById(customerId, Id, trackChanges);
            if (booking is null) { throw new ResourceNotFoundException(Id); }

            return _mapper.Map<BookingDto>(booking);
        }
    }
}
