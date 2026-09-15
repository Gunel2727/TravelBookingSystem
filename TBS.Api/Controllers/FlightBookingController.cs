using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TBS.Application.Common;
using TBS.Application.DTOs.FlightBooking;
using TBS.Application.Interfaces;

namespace TBS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightBookingController : ControllerBase
    {
        private readonly IFlightBookingService _flightBookingService;

        public FlightBookingController(IFlightBookingService flightBookingService)
        {
            _flightBookingService = flightBookingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings(CancellationToken cancellationToken = default)
        {
            var bookings = await _flightBookingService.GetAllAsync(cancellationToken);
            return Ok(ResponseModel<IList<FlightBookingDto>>.Ok(bookings));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(Guid id,CancellationToken cancellationToken=default)
        {
            var booking = await _flightBookingService.GetByIdAsync(id, cancellationToken);
            return Ok(ResponseModel<FlightBookingDto>.Ok(booking));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateFlightBookingDto dto, CancellationToken cancellationToken = default)
        {
            var booking = await _flightBookingService.AddAsync(dto, cancellationToken);
            return StatusCode(201, ResponseModel<FlightBookingDto>.Created(booking));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(Guid id, CancellationToken cancellationToken = default)
        {
            await _flightBookingService.DeleteAsync(id, cancellationToken);
            return Ok(ResponseModel<bool>.Ok(true));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(Guid id, [FromBody] UpdateFlightBookingDto dto,CancellationToken cancellationToken=default)
        {
            var booking=await _flightBookingService.UpdateAsync(id, dto, cancellationToken);
            return Ok(ResponseModel<FlightBookingDto>.Ok(booking));
        }
    }
}
