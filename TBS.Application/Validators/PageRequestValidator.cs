using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Common;

namespace TBS.Application.Validators
{
    public class PageRequestValidator:AbstractValidator<PageRequestDto>
    {
        public PageRequestValidator()
        {
            RuleFor(x => x.pageNumber)
                .GreaterThan(0).WithMessage("Səhifə nömrəsi 0-dan böyük olmalıdır.");
            RuleFor(x => x.pageSize)
                .GreaterThan(0).WithMessage("Səhifə ölçüsü 0-dan böyük olmalıdır.")
                .LessThanOrEqualTo(50).WithMessage("Səhifə ölçüsü ən çoxu 50 ola bilər.");
        }
    }
}
