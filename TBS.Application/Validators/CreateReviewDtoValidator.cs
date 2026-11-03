using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Review;

namespace TBS.Application.Validators
{
    public class CreateReviewDtoValidator:AbstractValidator<CreateReviewDto>
    {
        public CreateReviewDtoValidator()
        {
            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5)
                .WithMessage("Rating must be between 1 and 5.");
            RuleFor(x => x.Comment)
                .NotEmpty()
                .WithMessage("Comment is required.");
            RuleFor(x => x.HotelId)
                .NotEmpty()
                .WithMessage("HotelId is required.");
        }
    }
}
