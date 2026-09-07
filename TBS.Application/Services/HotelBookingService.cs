using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.Common;
using TBS.Application.DTOs.HotelBooking;
using TBS.Application.Interfaces;
using TBS.Domain.Entitites;
using TBS.Domain.Enums;
using TBS.Domain.Interfaces;

namespace TBS.Application.Services
{
    public class HotelBookingService : IHotelBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HotelBookingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<HotelBookingDto> AddAsync(CreateHotelBookingDto dto, CancellationToken cancellationToken = default)
        {
            if(dto.CheckInDate< DateTime.UtcNow) throw new BadRequestException(ErrorMessages.CheckInDateCannotBeInThePast);
            if(dto.CheckOutDate <= dto.CheckInDate) throw new BadRequestException(ErrorMessages.CheckOutDateMustBeAfterCheckInDate);
            int totalNights = (dto.CheckOutDate - dto.CheckInDate).Days;

            var bookingRooms = new List<HotelBookingRoom>();
            decimal totalNightlyPrice = 0;
            foreach(var roomId in dto.RoomIds)
            {
                bool isAvailable=await _unitOfWork.HotelBookingRepository.IsRoomAvailableAsync(roomId, dto.CheckInDate, dto.CheckOutDate);
                if(!isAvailable) {
                    throw new BadRequestException($"ID-si {roomId} olan otaq seçilmiş tarixlərdə məşğuldur.");
                }

                var room = await _unitOfWork.RoomRepository.GetByIdAsync(roomId);
                if(room==null)
                {
                    throw new NotFoundException($"ID-si {roomId} olan otaq tapılmadı.");
                }

                bookingRooms.Add(new HotelBookingRoom
                {
                    RoomId = roomId,
                    PriceForNight = room.PriceForNight
                });
                totalNightlyPrice += room.PriceForNight;
            }
            decimal totalPrice = totalNightlyPrice * totalNights;

            var hotelBooking = _mapper.Map<HotelBooking>(dto);
            hotelBooking.Status = BookingStatus.Pending;
            hotelBooking.ConfirmationNumber = $"HB-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
            hotelBooking.TotalPrice = totalPrice;
            hotelBooking.HotelBookingRooms = bookingRooms;

            await _unitOfWork.HotelBookingRepository.AddAsync(hotelBooking);
            await _unitOfWork.SaveChangesAsync();

            
            var createdBooking = await _unitOfWork.HotelBookingRepository.GetByIdAsync(hotelBooking.Id);
            return _mapper.Map<HotelBookingDto>(createdBooking ?? hotelBooking);
        }

        public async Task CancelBookingAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var hotelBooking = await _unitOfWork.HotelBookingRepository.GetByIdAsync(id);
            if (hotelBooking == null) throw new NotFoundException(ErrorMessages.HotelBookingNotFound);
            hotelBooking.Status = BookingStatus.Cancelled;
            _unitOfWork.HotelBookingRepository.Update(hotelBooking);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IList<HotelBookingDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var hotelBookings=await _unitOfWork.HotelBookingRepository.GetAllAsync();
            return _mapper.Map<IList<HotelBookingDto>>(hotelBookings);
        }

        public async Task<HotelBookingDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var hotelBooking = await _unitOfWork.HotelBookingRepository.GetByIdAsync(id);
            if (hotelBooking == null) throw new NotFoundException(ErrorMessages.HotelBookingNotFound);
            return _mapper.Map<HotelBookingDto>(hotelBooking);
        }

        public async Task<bool> IsRoomAvailableAsync(Guid roomId, DateTime checkInDate, DateTime checkOutDate, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.HotelBookingRepository.IsRoomAvailableAsync(roomId, checkInDate, checkOutDate);
        }

        public async Task<HotelBookingDto> UpdateAsync(Guid id, UpdateHotelBookingDto dto, CancellationToken cancellationToken = default)
        {
            var hotelBooking = await _unitOfWork.HotelBookingRepository.GetByIdAsync(id);
            if(hotelBooking == null) throw new NotFoundException(ErrorMessages.HotelBookingNotFound);
            _mapper.Map(dto, hotelBooking);
            _unitOfWork.HotelBookingRepository.Update(hotelBooking);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<HotelBookingDto>(hotelBooking);
        }
    }
}
