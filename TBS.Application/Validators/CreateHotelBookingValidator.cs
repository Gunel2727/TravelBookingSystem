using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.HotelBooking;

namespace TBS.Application.Validators
{
    public class CreateHotelBookingValidator : AbstractValidator<CreateHotelBookingDto>
    {
        public CreateHotelBookingValidator()
        {
            RuleFor(hb => hb.RoomIds)
                .NotEmpty().WithMessage("Otaq seçilməlidir.");

            RuleForEach(hb => hb.RoomIds)
                .NotEmpty().WithMessage("Roomid bos ola bilmez");

            RuleFor(hb => hb.CheckInDate)
                .GreaterThanOrEqualTo(hb => DateTime.UtcNow.Date).WithMessage("Giriş tarixi bu gündən və ya gələcək tarixdən olmalıdır.");

            RuleFor(hb => hb.CheckOutDate)
                .GreaterThan(hb => hb.CheckInDate).WithMessage("Çıxış tarixi giriş tarixindən sonra olmalıdır.");

          
        }
    }
}
