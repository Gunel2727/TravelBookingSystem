using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Flight;

namespace TBS.Application.Validators
{
    public class UpdateFlightValidator:AbstractValidator<UpdateFlightDto>
    {
        public UpdateFlightValidator()
        {
            RuleFor(f => f.FlightNumber)
                .NotEmpty().WithMessage("Uçuş nömrəsi boş ola bilməz.")
                .MaximumLength(20).WithMessage("Uçuş nömrəsi ən çoxu 20 simvol ola bilər.");

            RuleFor(f => f.AirlineId)
                .NotEmpty().WithMessage("Aviaşirkət seçilməlidir.");

            RuleFor(f => f.DepartureAirportId)
                .NotEmpty().WithMessage("Qalxış aeroportu seçilməlidir.")
                .NotEqual(f => f.ArrivalAirportId).WithMessage("Qalxış aeroportu ilə eniş aeroportu eyni ola bilməz.");

            RuleFor(f => f.ArrivalAirportId)
                .NotEmpty().WithMessage("Eniş aeroportu seçilməlidir.");

            RuleFor(f => f.Price)
                .GreaterThan(0).WithMessage("Qiymət 0-dan böyük olmalıdır.");

            RuleFor(f => f.AvailableSeats)
                .GreaterThan(0).WithMessage("Boş yerlərin sayı 0-dan böyük olmalıdır.");

            RuleFor(f => f.DepartureTime)
                .GreaterThanOrEqualTo(f => DateTime.UtcNow).WithMessage("Qalxış vaxtı cari vaxtdan və ya gələcəkdən olmalıdır.");

            RuleFor(f => f.ArrivalTime)
                .GreaterThan(f => f.DepartureTime).WithMessage("Eniş vaxtı qalxış vaxtından sonra olmalıdır.");

            RuleFor(f => f.StopCount)
                .GreaterThanOrEqualTo(0).WithMessage("Dayanacaq (tranzit) sayı 0 və ya daha böyük olmalıdır.");
        }
    }
}
