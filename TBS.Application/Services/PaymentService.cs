using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.Common;
using TBS.Application.DTOs.Payment;
using TBS.Application.Interfaces;
using TBS.Domain.Entitites;
using TBS.Domain.Enums;
using TBS.Domain.Interfaces;

namespace TBS.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<PaymentDto> CreateHotelBookingPaymentAsync(CreateHotelBookingPaymentDto dto, CancellationToken cancellationToken = default)
        {
            if (dto.HotelBookingId == null)
                throw new BadRequestException("Otel rezervasiya ID-si göndərilməlidir.");
            var userId = _currentUserService.UserId;
            if (userId == Guid.Empty)
                throw new BadRequestException("İstifadəçi ID-si tapılmadı.");
            var hotelBooking = await _unitOfWork.HotelBookingRepository.GetByIdAsync(dto.HotelBookingId,cancellationToken);
            
            if (hotelBooking == null)
                throw new NotFoundException("Otel rezervasiya tapılmadı.");
           
            if (hotelBooking.ApplicationUserId != userId)
                throw new ForbiddenException("Bu rezervasiya üçün ödəniş etmək icazəniz yoxdur.");
            if (hotelBooking.Status == BookingStatus.Cancelled)
                throw new BadRequestException("Ləğv olunmuş rezervasiya üçün ödəniş edilə bilməz.");
            if(hotelBooking.TotalPrice != dto.Amount)
                throw new BadRequestException("Ödənilən məbləğ bronun ümumi qiyməti ilə üst-üstə düşmür.");
            hotelBooking.Status = BookingStatus.Confirmed;
           

            var payment = _mapper.Map<Payment>(dto);
            payment.HotelBookingId = dto.HotelBookingId;
            payment.PaymentDate = DateTime.UtcNow;
            payment.Status = PaymentStatus.Paid;
            hotelBooking.Status = BookingStatus.Confirmed;
            await _unitOfWork.PaymentRepository.AddAsync(payment);
             _unitOfWork.HotelBookingRepository.Update(hotelBooking);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<PaymentDto>(payment);
        }
        public async Task<PaymentDto> CreateFlightBookingPaymentAsync(CreateFlightBookingPaymentDto dto, CancellationToken cancellationToken = default)
        {
            if (dto.FlightBookingId == null)
                throw new BadRequestException("Uçuş rezervasiya ID-si göndərilməlidir.");
            var userId = _currentUserService.UserId;
            if (userId == Guid.Empty)
                throw new BadRequestException("İstifadəçi ID-si tapılmadı.");
            var flightBooking = await _unitOfWork.FlightBookingRepository.GetByIdAsync(dto.FlightBookingId, cancellationToken);
           
            if (flightBooking == null)
                throw new NotFoundException("Uçuş rezervasiya tapılmadı.");
           
            if (flightBooking.ApplicationUserId != userId)
                throw new ForbiddenException("Bu rezervasiya üçün ödəniş etmək icazəniz yoxdur.");
            if (flightBooking.Status == BookingStatus.Cancelled)
                throw new BadRequestException("Ləğv olunmuş rezervasiya üçün ödəniş edilə bilməz.");
            if (flightBooking.TotalPrice != dto.Amount)
                throw new BadRequestException("Ödənilən məbləğ bronun ümumi qiyməti ilə üst-üstə düşmür.");
            flightBooking.Status = BookingStatus.Confirmed;
            var payment = _mapper.Map<Payment>(dto);
            payment.FlightBookingId = dto.FlightBookingId;
            payment.PaymentDate = DateTime.UtcNow;
            payment.Status = PaymentStatus.Paid;
            await _unitOfWork.PaymentRepository.AddAsync(payment);
            _unitOfWork.FlightBookingRepository.Update(flightBooking);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(id,cancellationToken);
            if(payment == null)
                throw new NotFoundException(ErrorMessages.PaymentNotFound);
            _unitOfWork.PaymentRepository.Delete(payment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<PaymentDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var payments= await _unitOfWork.PaymentRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IList<PaymentDto>>(payments);
        }

        public async Task<PaymentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var payment =await _unitOfWork.PaymentRepository.GetByIdAsync(id,cancellationToken);
            if (payment == null)
                throw new NotFoundException(ErrorMessages.PaymentNotFound);
            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<PaymentDto> UpdateAsync(Guid id, UpdatePaymentDto dto, CancellationToken cancellationToken = default)
        {
            var payment=await _unitOfWork.PaymentRepository.GetByIdAsync(id,cancellationToken);
            if (payment == null)
                throw new NotFoundException(ErrorMessages.PaymentNotFound);
            _mapper.Map(dto, payment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
           
            return _mapper.Map<PaymentDto>(payment);

        }
    }
}
