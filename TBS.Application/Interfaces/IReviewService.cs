using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Review;

namespace TBS.Application.Interfaces
{
    public interface IReviewService
    {
        Task<ReviewDto> AddAsync(CreateReviewDto createReviewDto,CancellationToken cancellationToken=default);
        Task<IList<ReviewDto>> GetReviewsByHotelIdAsync(Guid hotelId,CancellationToken cancellationToken=default);
        Task DeleteAsync(Guid reviewId, CancellationToken cancellationToken = default);
    }
}
