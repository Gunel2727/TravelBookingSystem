using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TBS.Application.Common;
using TBS.Application.DTOs.HotelBooking;
using TBS.Application.Interfaces;

namespace TBS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelBookingController : ControllerBase
    {
        private readonly IHotelBookingService _hotelBookingService;

        public HotelBookingController(IHotelBookingService hotelBookingService)
        {
            _hotelBookingService = hotelBookingService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllBookings(CancellationToken cancellationToken)
        {
            var bookings= await _hotelBookingService.GetAllAsync(cancellationToken);
            return Ok(ResponseModel<IList<HotelBookingDto>>.Ok(bookings));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetBookingById(Guid id, CancellationToken cancellationToken)
        {
            var booking = await _hotelBookingService.GetByIdAsync(id,cancellationToken);
            return Ok(ResponseModel<HotelBookingDto>.Ok(booking));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBooking([FromBody] CreateHotelBookingDto dto, CancellationToken cancellationToken)
        {
            var booking = await _hotelBookingService.AddAsync(dto, cancellationToken);
            return StatusCode(201, ResponseModel<HotelBookingDto>.Created(booking));
        }

        [HttpPatch("{id:guid}/cancel")]
        [Authorize] 
        public async Task<IActionResult> CancelBooking(Guid id, CancellationToken cancellationToken)
        {
            await _hotelBookingService.CancelBookingAsync(id, cancellationToken);
            return Ok(ResponseModel<bool>.Ok(true));
        }

        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateBooking(Guid id, [FromBody] UpdateHotelBookingDto dto, CancellationToken cancellationToken)
        {
            var booking = await _hotelBookingService.UpdateAsync(id, dto, cancellationToken);
            return Ok(ResponseModel<HotelBookingDto>.Ok(booking));
        }

        [HttpGet("isroomavailable")]
        public async Task<IActionResult> IsRoomAvailable([FromQuery] Guid roomId, [FromQuery] DateTime checkInDate, [FromQuery] DateTime checkOutDate, CancellationToken cancellationToken)
        {
            var isAvailable = await _hotelBookingService.IsRoomAvailableAsync(roomId, checkInDate, checkOutDate, cancellationToken);
            return Ok(ResponseModel<bool>.Ok(isAvailable));
        }

        [HttpGet("my-bookings")]
        [Authorize]
        public async Task<IActionResult> GetMyBookings(CancellationToken cancellationToken)
        {
            var bookings = await _hotelBookingService.GetBookingsByUserIdAsync(cancellationToken);
            return Ok(ResponseModel<IList<HotelBookingDto>>.Ok(bookings));
        }
    }
}
