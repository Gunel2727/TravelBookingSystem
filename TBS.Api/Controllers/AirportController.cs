using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TBS.Application.Common;
using TBS.Application.DTOs.Airport;
using TBS.Application.Interfaces;

namespace TBS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IAirportService _airportService;

        public AirportController(IAirportService airportService)
        {
            _airportService = airportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAirports(CancellationToken cancellationToken)
        {
            var airports = await _airportService.GetAllAsync(cancellationToken);
            return Ok(ResponseModel<IList<AirportDto>>.Ok(airports));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByAirportId(Guid id, CancellationToken cancellationToken)
        {
            var airport = await _airportService.GetByIdAsync(id, cancellationToken);
            return Ok(ResponseModel<AirportDto>.Ok(airport));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAirport([FromBody] CreateAirportDto dto, CancellationToken cancellationToken)
        {
            var airport = await _airportService.CreateAsync(dto, cancellationToken);
            return StatusCode(201, ResponseModel<AirportDto>.Created(airport));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAirport(Guid id, [FromBody] UpdateAirportDto dto,CancellationToken cancellationToken)
        {
            var airport= await _airportService.UpdateAsync(id, dto, cancellationToken);
            return Ok(ResponseModel<AirportDto>.Ok(airport));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAirport(Guid id, CancellationToken cancellationToken)
        {
            await _airportService.DeleteAsync(id, cancellationToken);
            return Ok(ResponseModel<bool>.Ok(true));
        }


    }
}
