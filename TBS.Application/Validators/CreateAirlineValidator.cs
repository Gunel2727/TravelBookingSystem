using FluentValidation;
using TBS.Application.DTOs.Airline;

namespace TBS.Application.Validators
{
    public class CreateAirlineValidator : AbstractValidator<CreateAirlineDto>
    {
        public CreateAirlineValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("Aviaşirkət adı boş ola bilməz.")
                .MaximumLength(100).WithMessage("Aviaşirkət adı ən çoxu 100 simvol ola bilər.");

            RuleFor(a => a.Code)
                .NotEmpty().WithMessage("Aviaşirkət kodu boş ola bilməz.")
                .Length(2, 3).WithMessage("Aviaşirkət kodu 2 və ya 3 simvoldan ibarət olmalıdır.")
                .Matches(@"^[A-Z0-9]{2,3}$").WithMessage("Aviaşirkət kodu yalnız böyük hərflərdən və rəqəmlərdən ibarət ola bilər (məs: J2, TK).");
        }
    }
}