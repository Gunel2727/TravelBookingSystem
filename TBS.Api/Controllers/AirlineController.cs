using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TBS.Application.Common;
using TBS.Application.DTOs.Airline;
using TBS.Application.Interfaces;


namespace TBS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
        private readonly IAirlineService _airlineService;

        public AirlineController(IAirlineService airlineService)
        {
            _airlineService = airlineService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAirlines(CancellationToken cancellationToken = default)
        {
            var airlines = await _airlineService.GetAllAsync(cancellationToken);
            return Ok(ResponseModel<IList<AirlineDto>>.Ok(airlines));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByAirlineId(Guid id,CancellationToken cancellationToken=default)
        {
            var airline = await _airlineService.GetByIdAsync(id, cancellationToken);
            return Ok(ResponseModel<AirlineDto>.Ok(airline));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAirline([FromBody] CreateAirlineDto dto,CancellationToken cancellationToken=default)
        {
            var airline = await _airlineService.CreateAsync(dto, cancellationToken);
            return StatusCode(201, ResponseModel<AirlineDto>.Created(airline));
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAirline(Guid id, [FromBody] UpdateAirlineDto dto,CancellationToken cancellationToken=default)
        {
            var airline = await _airlineService.UpdateAsync(id, dto, cancellationToken);
            return Ok(ResponseModel<AirlineDto>.Ok(airline));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAirline(Guid id,CancellationToken cancellationToken=default)
        {
            await _airlineService.DeleteAsync(id, cancellationToken);
            return Ok(ResponseModel<bool>.Ok(true));
        }
    }
}
