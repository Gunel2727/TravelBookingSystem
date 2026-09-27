using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Auth;

namespace TBS.Application.Validators
{
    public class RegisterValidator:AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("Ad boş ola bilməz.")
                .MinimumLength(2).WithMessage("Ad ən azı 2 simvol olmalıdır.")
                .MaximumLength(50).WithMessage("Ad ən çoxu 50 simvol ola bilər.");

            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Soyad boş ola bilməz.")
                .MinimumLength(2).WithMessage("Soyad ən azı 2 simvol olmalıdır.")
                .MaximumLength(50).WithMessage("Soyad ən çoxu 50 simvol ola bilər.");

            RuleFor(r => r.Email)
                .NotEmpty()
                .EmailAddress().WithMessage("Düzgün e-poçt formatı daxil edin.");

            RuleFor(r => r.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches(@"[A-Z]").WithMessage("Şifrədə ən azı bir böyük hərf olmalıdır.")
                .Matches(@"[a-z]").WithMessage("Şifrədə ən azı bir kicik hərf olmalıdır.")
                .Matches(@"[0-9]").WithMessage("Şifrədə ən azı bir reqem hərf olmalıdır.");

            RuleFor(r => r.UserName)
                .NotEmpty().WithMessage("Istifadeci adi boş ola bilməz.")
                .MinimumLength(2).WithMessage("Istifadeci adi ən azı 2 simvol olmalıdır.")
                .MaximumLength(50).WithMessage("Istifadeci adi ən çoxu 50 simvol ola bilər.");

            RuleFor(r => r.ConfirmPassword)
                .Equal(r => r.Password).WithMessage("Şifrə ilə şifrə təkrarı üst-üstə düşmür.");

            RuleFor(r => r.PhoneNumber)
               .Matches(@"^\+?[0-9]{9,15}$").WithMessage("Düzgün telefon nömrəsi daxil edin.");


        }
    }
}
