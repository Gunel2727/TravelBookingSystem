using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Hotel;

namespace TBS.Application.Validators
{
    public class UpdateHotelValidator:AbstractValidator<UpdateHotelDto>
    {
        public UpdateHotelValidator()
        {
            RuleFor(h => h.Name)
                .NotEmpty().WithMessage("Otel adı boş ola bilməz.")
                .MaximumLength(150).WithMessage("Otel adı ən çoxu 150 simvol ola bilər.");

            RuleFor(h => h.Description)
                .NotEmpty().WithMessage("Description boş ola bilməz.")
                .MaximumLength(500).WithMessage("Description ən çoxu 500 simvol ola bilər.");


            RuleFor(h => h.Address)
                .NotEmpty().WithMessage("Ünvan boş ola bilməz.")
                .MaximumLength(250).WithMessage("Ünvan ən çoxu 250 simvol ola bilər.");

            RuleFor(h => h.StarRating)
                .InclusiveBetween(1, 5).WithMessage("Otel ulduzu 1 ilə 5 arasında olmalıdır.");

            RuleFor(h => h.CityId)
                .NotEmpty().WithMessage("Şəhər seçilməlidir.");
        }
    }
}
