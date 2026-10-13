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
        private readonly ICurrentUserService _currentUserService;
        public HotelBookingService(IUnitOfWork unitOfWork, IMapper mapper,ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public async Task<HotelBookingDto> AddAsync(CreateHotelBookingDto dto, CancellationToken cancellationToken = default)
        {
            if(dto.CheckInDate< DateTime.UtcNow) throw new BadRequestException(ErrorMessages.CheckInDateCannotBeInThePast);
            if(dto.CheckOutDate <= dto.CheckInDate) throw new BadRequestException(ErrorMessages.CheckOutDateMustBeAfterCheckInDate);

            if(dto.RoomIds==null || !dto.RoomIds.Any())
            {
                throw new BadRequestException("Ən azı bir otaq seçilməlidir.");
            }
            if(dto.RoomIds.Count()!=dto.RoomIds.Distinct().Count())
            {
                throw new BadRequestException("Eyni otaq bir neçə dəfə seçilə bilməz.");
            }
            
            int totalNights = (dto.CheckOutDate.Date - dto.CheckInDate.Date).Days;

            var bookingRooms = new List<HotelBookingRoom>();
            decimal totalNightlyPrice = 0;
            foreach(var roomId in dto.RoomIds)
            {
                bool isAvailable=await _unitOfWork.HotelBookingRepository.IsRoomAvailableAsync(roomId, dto.CheckInDate, dto.CheckOutDate,cancellationToken);
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
            hotelBooking.ApplicationUserId = _currentUserService.UserId;
            hotelBooking.Status = BookingStatus.Pending;
            hotelBooking.ConfirmationNumber = $"HB-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
            hotelBooking.TotalPrice = totalPrice;
            hotelBooking.HotelBookingRooms = bookingRooms;

            await _unitOfWork.HotelBookingRepository.AddAsync(hotelBooking);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            
            var createdBooking = await _unitOfWork.HotelBookingRepository.GetByIdAsync(hotelBooking.Id,cancellationToken);
            return _mapper.Map<HotelBookingDto>(createdBooking ?? hotelBooking);
        }

        public async Task CancelBookingAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var hotelBooking = await _unitOfWork.HotelBookingRepository.GetByIdAsync(id,cancellationToken);
            if (hotelBooking == null) throw new NotFoundException(ErrorMessages.HotelBookingNotFound);

            if(hotelBooking.Status == BookingStatus.Cancelled)
            {
                throw new BadRequestException("Rezervasiya artıq ləğv edilib.");
            }
            if (hotelBooking.CheckInDate <= DateTime.UtcNow)
                throw new BadRequestException("Giriş tarixi başlamış və ya keçmiş bronlar ləğv edilə bilməz.");
            hotelBooking.Status = BookingStatus.Cancelled;
            _unitOfWork.HotelBookingRepository.Update(hotelBooking);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<HotelBookingDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var hotelBookings=await _unitOfWork.HotelBookingRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IList<HotelBookingDto>>(hotelBookings);
        }

        public async Task<IList<HotelBookingDto>> GetBookingsByUserIdAsync(CancellationToken cancellationToken = default)
        {
            var userId = _currentUserService.UserId;
            if(userId == Guid.Empty) throw new UnauthorizedException("Sistemə daxil olmalısınız.");
            var hotelBookings = await _unitOfWork.HotelBookingRepository.GetBookingsByUserIdAsync(userId, cancellationToken);
            return _mapper.Map<IList<HotelBookingDto>>(hotelBookings);
        }

        public async Task<HotelBookingDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var hotelBooking = await _unitOfWork.HotelBookingRepository.GetByIdAsync(id,cancellationToken);
            if (hotelBooking == null) throw new NotFoundException(ErrorMessages.HotelBookingNotFound);
            return _mapper.Map<HotelBookingDto>(hotelBooking);
        }

        public async Task<bool> IsRoomAvailableAsync(Guid roomId, DateTime checkInDate, DateTime checkOutDate, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.HotelBookingRepository.IsRoomAvailableAsync(roomId, checkInDate, checkOutDate,cancellationToken);
        }

        public async Task<HotelBookingDto> UpdateAsync(Guid id, UpdateHotelBookingDto dto, CancellationToken cancellationToken = default)
        {
            var hotelBooking = await _unitOfWork.HotelBookingRepository.GetByIdAsync(id);
            if(hotelBooking == null) throw new NotFoundException(ErrorMessages.HotelBookingNotFound);

            if(hotelBooking.ApplicationUserId!=_currentUserService.UserId)
            {
                throw new ForbiddenException("Siz yalnız özünüzə aid rezervasiyaları yeniləyə bilərsiniz.");
            }
        
            _mapper.Map(dto, hotelBooking);
            _unitOfWork.HotelBookingRepository.Update(hotelBooking);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<HotelBookingDto>(hotelBooking);
        }
    }
}
