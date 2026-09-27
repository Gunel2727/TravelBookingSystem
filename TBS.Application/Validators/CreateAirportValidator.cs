using FluentValidation;
using System;
using TBS.Application.DTOs.Airport;

namespace TBS.Application.Validators
{
    public class CreateAirportValidator : AbstractValidator<CreateAirportDto>
    {
        public CreateAirportValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("Aeroport adı boş ola bilməz.")
                .MaximumLength(100).WithMessage("Aeroport adı ən çoxu 100 simvol ola bilər.");

            RuleFor(a => a.Code)
                .NotEmpty().WithMessage("Aeroport kodu boş ola bilməz.")
                .Length(3).WithMessage("Aeroport kodu dəqiq 3 simvol olmalıdır.")
                .Matches(@"^[A-Z]{3}$").WithMessage("Aeroport kodu 3 böyük ingilis hərfindən ibarət olmalıdır (məs: GYD).");

            RuleFor(a => a.CityId)
                .NotEmpty().WithMessage("Şəhər seçilməlidir.");
        }
    }
}