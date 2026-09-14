using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.Common;
using TBS.Application.DTOs.FlightBooking;
using TBS.Application.DTOs.HotelBooking;
using TBS.Application.Interfaces;
using TBS.Domain.Entitites;
using TBS.Domain.Enums;
using TBS.Domain.Interfaces;

namespace TBS.Application.Services
{
    public class FlightBookingService : IFlightBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FlightBookingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FlightBookingDto> AddAsync(CreateFlightBookingDto dto, CancellationToken cancellationToken = default)
        {
            if(!dto.PassengerIds.Any())
            {
                throw new BadRequestException("Ən azı bir sərnişin seçilməlidir.");

            }
            if(dto.PassengerIds.Distinct().Count() != dto.PassengerIds.Count)
            {
                throw new BadRequestException("Sərnişinlər təkrarlana bilməz.");
            }
            var flight=await _unitOfWork.FlightRepository.GetByIdAsync(dto.FlightId);
            if (flight == null)
            {
                throw new NotFoundException("Uçuş tapılmadı.");
            }
          if(dto.PassengerIds.Count > flight.AvailableSeats)
            {
                throw new BadRequestException($"Uçuşda kifayət qədər boş yer yoxdur. Qalan yer sayı: {flight.AvailableSeats}");

            }
            var flightBooking = _mapper.Map<FlightBooking>(dto);

            flightBooking.ConfirmationNumber = $"FB-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
            flightBooking.FlightBookingNumber = $"FBN-{Guid.NewGuid().ToString("N")[..6].ToUpper()}";
            flightBooking.BookingDate = DateTime.UtcNow;
            flightBooking.Status = BookingStatus.Pending;
            flightBooking.TotalPrice = flight.Price * dto.PassengerIds.Count;

            flightBooking.FlightBookingPassengers=dto.PassengerIds.Select(passengerId => new FlightBookingPassenger
            {
                PassengerId = passengerId
               
            }).ToList();

            flight.AvailableSeats -= dto.PassengerIds.Count;
            _unitOfWork.FlightRepository.Update(flight);

            await _unitOfWork.FlightBookingRepository.AddAsync(flightBooking);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var createdFlightBooking = await _unitOfWork.FlightBookingRepository.GetByIdAsync(flightBooking.Id);
            return _mapper.Map<FlightBookingDto>(createdFlightBooking ?? flightBooking);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var flightBooking = await _unitOfWork.FlightBookingRepository.GetByIdAsync(id);
            if(flightBooking == null)
            {
                throw new NotFoundException(ErrorMessages.FlightBookingNotFound);
            }
            if (flightBooking.Flight != null && flightBooking.FlightBookingPassengers != null)
            {
                flightBooking.Flight.AvailableSeats += flightBooking.FlightBookingPassengers.Count;
                _unitOfWork.FlightRepository.Update(flightBooking.Flight);
            }

            _unitOfWork.FlightBookingRepository.Delete(flightBooking);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<FlightBookingDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var flightBooking=await _unitOfWork.FlightBookingRepository.GetAllAsync();
            return _mapper.Map<IList<FlightBookingDto>>(flightBooking);
        }

        public async Task<FlightBookingDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var flightBooking=await _unitOfWork.FlightBookingRepository.GetByIdAsync(id);
            if (flightBooking==null)
            {
                throw new NotFoundException(ErrorMessages.FlightBookingNotFound);
            }
            return _mapper.Map<FlightBookingDto>(flightBooking);
        }

        public async Task<FlightBookingDto> UpdateAsync(Guid id, UpdateFlightBookingDto dto, CancellationToken cancellationToken = default)
        {
            var flightBooking = await _unitOfWork.FlightBookingRepository.GetByIdAsync(id);
            if (flightBooking==null)
            {
                throw new NotFoundException(ErrorMessages.FlightBookingNotFound);
            }

           
            _mapper.Map(dto, flightBooking);
            _unitOfWork.FlightBookingRepository.Update(flightBooking);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<FlightBookingDto>(flightBooking);
        }
    }
}
