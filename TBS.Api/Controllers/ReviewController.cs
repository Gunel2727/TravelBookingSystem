using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TBS.Application.Common;
using TBS.Application.DTOs.Review;
using TBS.Application.Interfaces;

namespace TBS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateReviewAsync([FromBody] CreateReviewDto createReviewDto, CancellationToken cancellationToken = default)
        {
            var review =await  _reviewService.AddAsync(createReviewDto, cancellationToken);
            return StatusCode(201, ResponseModel<ReviewDto>.Created(review));
        }

        [HttpGet("{hotelId:guid}")]
        public async Task<IActionResult> GetReviewsByHotelIdAsync(Guid hotelId,CancellationToken cancellationToken =default)
        {
            var reviews=await _reviewService.GetReviewsByHotelIdAsync(hotelId, cancellationToken);
            return Ok(ResponseModel<IList<ReviewDto>>.Ok(reviews));
        }

        [HttpDelete("{reviewId:guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteReviewAsync(Guid reviewId, CancellationToken cancellationToken = default)
        {
            await _reviewService.DeleteAsync(reviewId, cancellationToken);
            return Ok(ResponseModel<bool>.Ok(true));
        }

    }
}
