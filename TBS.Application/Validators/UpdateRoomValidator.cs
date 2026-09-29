using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Room;
using TBS.Domain.Enums;

namespace TBS.Application.Validators
{
    public class UpdateRoomValidator : AbstractValidator<UpdateRoomDto>
    {
        public UpdateRoomValidator()
        {
           
            RuleFor(r => r.RoomNumber)
                .NotEmpty().WithMessage("Otaq nömrəsi boş ola bilməz.")
                .MaximumLength(20).WithMessage("Otaq nömrəsi ən çoxu 20 simvol ola bilər.");

            RuleFor(r => r.PriceForNight)
                .GreaterThan(0).WithMessage("Gecəlik qiymət 0-dan böyük olmalıdır.");

            RuleFor(r => r.Capacity)
                .GreaterThan(0).WithMessage("Otaq tutumu ən azı 1 nəfərlik olmalıdır.");

            RuleFor(r => r.Name)
               .NotEmpty().WithMessage("Otaq adı boş ola bilməz.")
               .MaximumLength(150).WithMessage("Otel adı ən çoxu 150 simvol ola bilər.");

            RuleFor(r => r.Description)
                .NotEmpty().WithMessage("Description boş ola bilməz.")
                .MaximumLength(500).WithMessage("Description ən çoxu 500 simvol ola bilər.");


            RuleFor(r => r.RoomType)
                .NotEmpty().WithMessage("Otaq tipi boş ola bilməz.")
                .IsEnumName(typeof(RoomType), caseSensitive: false)
                .WithMessage("Düzgün otaq tipi daxil edilməlidir (məs: Standard, Deluxe, Suite).");
        }
    }
    }
}
