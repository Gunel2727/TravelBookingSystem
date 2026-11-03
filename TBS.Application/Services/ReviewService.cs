using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Review;
using TBS.Application.Interfaces;
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
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid reviewId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ReviewDto>> GetReviewsByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
