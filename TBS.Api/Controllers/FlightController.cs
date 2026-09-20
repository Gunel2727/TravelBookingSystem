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
        public async Task<IActionResult> GetAllFlights(CancellationToken cancellationToken=default)
        {
            var flights = await _flightService.GetAllAsync(cancellationToken);
            return Ok(ResponseModel<IList<FlightDto>>.Ok(flights));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetFlightById(Guid id,CancellationToken cancellationToken=default)
        {
            var flight = await _flightService.GetByIdAsync(id,cancellationToken);
            return Ok(ResponseModel<FlightDto>.Ok(flight));
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlight([FromBody] CreateFlightDto dto,CancellationToken cancellationToken=default)
        {
            var flight = await _flightService.AddAsync(dto,cancellationToken);
            return StatusCode(201, ResponseModel<FlightDto>.Created(flight));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateFlight(Guid id, [FromBody] UpdateFlightDto dto,CancellationToken cancellationToken=default)
        {
            var flight = await _flightService.UpdateAsync(id, dto,cancellationToken);
            return Ok(ResponseModel<FlightDto>.Ok(flight));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteFlight(Guid id,CancellationToken cancellationToken=default)
        {
            await _flightService.DeleteAsync(id,cancellationToken);
            return Ok(ResponseModel<bool>.Ok(true));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterFlights([FromQuery] FilterFlightDto dto,CancellationToken cancellationToken=default)
        {
            var flights = await _flightService.GetFilteredAsync(dto,cancellationToken);
            return Ok(ResponseModel<IList<FlightDto>>.Ok(flights));
        }
    }
}
