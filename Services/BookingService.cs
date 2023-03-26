using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
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

        public async Task<IEnumerable<BookingDto>> GetAllAsync(Guid customerId, bool trackChanges)
        {
            var customer = await _repositoryManager.CustomerRepository.GetByIdAsync(customerId, trackChanges);
            if (customer == null) { throw new ResourceNotFoundException(customerId); }

            var bookings = await _repositoryManager.BookingRepository.GetAllAsync(customerId, trackChanges);
            return _mapper.Map<IEnumerable<BookingDto>>(bookings);
        }

        public async Task<BookingDto> GetByIdAsync(Guid customerId, Guid Id, bool trackChanges)
        {
            var customer = await _repositoryManager.CustomerRepository.GetByIdAsync(customerId, trackChanges);
            if (customer == null) { throw new ResourceNotFoundException(customerId); }

            var booking = await _repositoryManager.BookingRepository.GetByIdAsync(customerId, Id, trackChanges);
            if (booking is null) { throw new ResourceNotFoundException(Id); }

            return _mapper.Map<BookingDto>(booking);
        }

        public async Task<BookingDto> CreateAsync(Guid customerId, BookingCreationDto bookingCreationDto, bool trackChanges)
        {
            var customer = await _repositoryManager.CustomerRepository.GetByIdAsync(customerId, trackChanges: trackChanges);
            if (customer == null) { throw new ResourceNotFoundException(customerId); }

            var vehicle = await _repositoryManager.VehicleRepository.GetByIdAsync(bookingCreationDto.VehicleId, trackChanges: trackChanges);
            if (vehicle == null) { throw new ResourceNotFoundException(bookingCreationDto.VehicleId); }

            var booking = _mapper.Map<Booking>(bookingCreationDto);

            _repositoryManager.BookingRepository.Create(customer.Id, booking);
            await _repositoryManager.SaveAsync();

            return _mapper.Map<BookingDto>(booking);
        }

        public async Task DeleteBookingAsync(Guid customerId, Guid Id, bool trackChanges)
        {
            var customer = await _repositoryManager.CustomerRepository.GetByIdAsync(customerId, trackChanges);
            if (customer == null) { throw new ResourceNotFoundException(customerId); }

            var booking = await _repositoryManager.BookingRepository.GetByIdAsync(customerId, Id, trackChanges);
            if (booking == null) { throw new ResourceNotFoundException(Id); }

            _repositoryManager.BookingRepository.DeleteBooking(booking);
            await _repositoryManager.SaveAsync();
        }
    }
}
