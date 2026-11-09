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
    public class CreateHotelBookingPaymentValidator : AbstractValidator<CreateHotelBookingPaymentDto>
    {
        public CreateHotelBookingPaymentValidator()
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

            RuleFor(p => p.HotelBookingId)
                .NotEmpty().WithMessage("Otel rezervasiya ID-si boş ola bilməz.");


        }
    }
}
