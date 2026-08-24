using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TBS.Application.Common;
using TBS.Application.DTOs.Flight;
using TBS.Application.Interfaces;

namespace TBS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFlights()
        {
            var flights = await _flightService.GetAllAsync();
            return Ok(ResponseModel<IList<FlightDto>>.Ok(flights));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlightById(Guid id)
        {
            var flight = await _flightService.GetByIdAsync(id);
            return Ok(ResponseModel<FlightDto>.Ok(flight));
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlight([FromBody] CreateFlightDto dto)
        {
            var flight = await _flightService.AddAsync(dto);
            return StatusCode(201, ResponseModel<FlightDto>.Created(flight));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlight(Guid id, [FromBody] UpdateFlightDto dto)
        {
            var flight = await _flightService.UpdateAsync(id, dto);
            return Ok(ResponseModel<FlightDto>.Ok(flight));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(Guid id)
        {
            await _flightService.DeleteAsync(id);
            return Ok(ResponseModel<bool>.Ok(true));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterFlights([FromQuery] FilterFlightDto dto)
        {
            var flights = await _flightService.GetFilteredAsync(dto);
            return Ok(ResponseModel<IList<FlightDto>>.Ok(flights));
        }
    }
}
