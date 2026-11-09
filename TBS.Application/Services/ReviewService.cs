using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.Common;
using TBS.Application.DTOs.Review;
using TBS.Application.Interfaces;
using TBS.Domain.Entitites;
using TBS.Domain.Interfaces;

namespace TBS.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper,ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public async Task<ReviewDto> AddAsync(CreateReviewDto createReviewDto,CancellationToken cancellationToken=default)
        {
            var userId = _currentUserService.UserId;
            if(userId == Guid.Empty)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            var existingHotel=await _unitOfWork.HotelRepository.GetByIdAsync(createReviewDto.HotelId, cancellationToken);
            if(existingHotel == null)
            {
                throw new ArgumentException("Hotel not found.");
            }
            var reviews=await _unitOfWork.ReviewRepository.GetReviewsByHotelIdAsync(createReviewDto.HotelId, cancellationToken);
            if(reviews.Any(r => r.ApplicationUserId == userId))
            {
                throw new BadRequestException(ErrorMessages.ReviewAlreadyExists);
            }
            var review = _mapper.Map<Review>(createReviewDto);
            review.ApplicationUserId = userId;
            await _unitOfWork.ReviewRepository.AddAsync(review);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ReviewDto>(review);

        }

        public async Task DeleteAsync(Guid reviewId, CancellationToken cancellationToken = default)
        {
            var userId = _currentUserService.UserId;
            var review = await _unitOfWork.ReviewRepository.GetByIdAsync(reviewId, cancellationToken);
            if (review == null)
            {
                throw new NotFoundException(ErrorMessages.ReviewNotFound);
            }
            if (review.ApplicationUserId != userId)
            {
                throw new ForbiddenException(ErrorMessages.ReviewOwnershipError);
            }
             _unitOfWork.ReviewRepository.Delete(review);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<ReviewDto>> GetReviewsByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default)
        {
            var reviews = await _unitOfWork.ReviewRepository.GetReviewsByHotelIdAsync(hotelId, cancellationToken);
            return _mapper.Map<IList<ReviewDto>>(reviews);

        }
    }
}
