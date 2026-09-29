using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.FlightBooking;

namespace TBS.Application.Validators
{
    public class CreateFlightBookingValidator:AbstractValidator<CreateFlightBookingDto>
    {
        public CreateFlightBookingValidator()
        {
            RuleFor(fb => fb.FlightId)
                .NotEmpty().WithMessage("Flight id bos ola bilmez");

            RuleFor(fb => fb.PassengerIds)
                .NotEmpty().WithMessage("Ən azı bir sərnişin seçilməlidir.");

            RuleForEach(fb => fb.PassengerIds)
                .NotEmpty().WithMessage("Passenger ids bos ola bilmez");

            
        }
    }
}
