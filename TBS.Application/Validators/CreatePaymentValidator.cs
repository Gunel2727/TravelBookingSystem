using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Payment;
using TBS.Domain.Enums;

namespace TBS.Application.Validators
{
    public class CreatePaymentValidator : AbstractValidator<CreatePaymentDto>
    {
        public CreatePaymentValidator()
        {
            RuleFor(p => p.Amount)
                .GreaterThan(0).WithMessage("Ödəniş məbləği 0-dan böyük olmalıdır.");

            RuleFor(p => p.PaymentMethod)
                .NotEmpty().WithMessage("Ödəniş üsulu boş ola bilməz.")
                .MaximumLength(50).WithMessage("Ödəniş üsulu ən çoxu 50 simvol ola bilər.");

            RuleFor(p => p.Currency)
                .NotEmpty().WithMessage("Valyuta boş ola bilməz.")
                .Length(3).WithMessage("Valyuta kodu dəqiq 3 simvol olmalıdır.")
                .Matches(@"^[A-Z]{3}$").WithMessage("Valyuta kodu 3 böyük ingilis hərfindən ibarət olmalıdır (məs: AZN, USD, EUR).");

            
            RuleFor(p => p)
                .Must(p => (p.FlightBookingId.HasValue && p.FlightBookingId.Value != Guid.Empty) ||
                           (p.HotelBookingId.HasValue && p.HotelBookingId.Value != Guid.Empty))
                .WithMessage("Ödəniş üçün mütləq ya Uçuş Bronu (FlightBookingId), ya da Otel Bronu (HotelBookingId) seçilməlidir.");

            RuleFor(p => p)
                .Must(p => !(p.FlightBookingId.HasValue && p.HotelBookingId.HasValue))
                .WithMessage("Ödəniş eyni vaxtda həm Uçuş, həm də Otel bronu üçün edilə bilməz. Yalnız birini seçin.");
        }
    }
}
