using FluentValidation;
using TBS.Application.DTOs.FlightBooking;

namespace TBS.Application.Validators
{
    public class CreateFlightBookingValidator : AbstractValidator<CreateFlightBookingDto>
    {
        public CreateFlightBookingValidator()
        {
            RuleFor(fb => fb.FlightId)
                .NotEmpty().WithMessage("Uçuş ID-si boş ola bilməz.");

            RuleFor(fb => fb.Passengers)
                .NotEmpty().WithMessage("Ən azı bir sərnişin daxil edilməlidir.");

            RuleForEach(fb => fb.Passengers)
                .SetValidator(new CreatePassengerValidator());
        }
    }
}