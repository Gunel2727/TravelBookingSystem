using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Passenger;

namespace TBS.Application.Validators
{
    public class CreatePassengerValidator: AbstractValidator<CreatePassengerDto>
    {
        public CreatePassengerValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("Sərnişinin adı boş ola bilməz.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("Sərnişinin soyadı boş ola bilməz.");

            RuleFor(p => p.PassportNumber)
                .NotEmpty().WithMessage("Pasport nömrəsi boş ola bilməz.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email boş ola bilməz.")
                .EmailAddress().WithMessage("Düzgün email formatı daxil edin.");

            RuleFor(p => p.DateOfBirth)
                .LessThan(DateTime.UtcNow).WithMessage("Doğum tarixi keçmiş tarix olmalıdır.");

            RuleFor(p => p.PassportExpiryDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Pasportun etibarlılıq müddəti bitmişdir.");
        }
    }
}
