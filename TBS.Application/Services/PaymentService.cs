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

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaymentDto> AddAsync(CreatePaymentDto dto, CancellationToken cancellationToken = default)
        {
            if (dto.HotelBookingId == null)
                throw new BadRequestException("Otel rezervasiya ID-si göndərilməlidir.");
            var hotelBooking = await _unitOfWork.HotelBookingRepository.GetByIdAsync(dto.HotelBookingId.Value);
            if(hotelBooking == null)
                throw new NotFoundException("Otel rezervasiya tapılmadı.");
            if(hotelBooking.Status == BookingStatus.Cancelled)
                throw new BadRequestException("Ləğv olunmuş rezervasiya üçün ödəniş edilə bilməz.");
            if(hotelBooking.Status == BookingStatus.Completed || hotelBooking.Status == BookingStatus.Confirmed)
                throw new BadRequestException("Tamamlanmış və ya təsdiqlənmiş rezervasiya üçün ödəniş edilə bilməz.");
            if(hotelBooking.TotalPrice != dto.Amount)
                throw new BadRequestException("Ödənilən məbləğ bronun ümumi qiyməti ilə üst-üstə düşmür.");
            hotelBooking.Status = BookingStatus.Confirmed;
            var payment = _mapper.Map<Payment>(dto);
            payment.PaymentDate = DateTime.UtcNow;
            payment.Status = PaymentStatus.Paid;
            await _unitOfWork.PaymentRepository.AddAsync(payment);
             _unitOfWork.HotelBookingRepository.Update(hotelBooking);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(id);
            if(payment == null)
                throw new NotFoundException(ErrorMessages.PaymentNotFound);
            _unitOfWork.PaymentRepository.Delete(payment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IList<PaymentDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var payments= await _unitOfWork.PaymentRepository.GetAllAsync();
            return _mapper.Map<IList<PaymentDto>>(payments);
        }

        public async Task<PaymentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var payment =await _unitOfWork.PaymentRepository.GetByIdAsync(id);
            if (payment == null)
                throw new NotFoundException(ErrorMessages.PaymentNotFound);
            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<PaymentDto> UpdateAsync(Guid id, UpdatePaymentDto dto, CancellationToken cancellationToken = default)
        {
            var payment=await _unitOfWork.PaymentRepository.GetByIdAsync(id);
            if (payment == null)
                throw new NotFoundException(ErrorMessages.PaymentNotFound);
            _mapper.Map(dto, payment);
            await _unitOfWork.SaveChangesAsync();
           
            return _mapper.Map<PaymentDto>(payment);

        }
    }
}
