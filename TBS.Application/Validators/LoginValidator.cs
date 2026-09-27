using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Auth;

namespace TBS.Application.Validators
{
    public class LoginValidator:AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(l => l.Email)
                .NotEmpty().WithMessage("Email boş ola bilməz.")
                .EmailAddress().WithMessage("Düzgün e-poçt formatı daxil edin.");

            RuleFor(l => l.Password)
                .NotEmpty().WithMessage("Sifre boş ola bilməz.");
        }
    }
}
