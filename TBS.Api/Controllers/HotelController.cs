using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TBS.Application.Common;
using TBS.Application.DTOs.Hotel;
using TBS.Application.Interfaces;

namespace TBS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        
        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels(CancellationToken cancellationToken = default)
        {
            var hotels= await _hotelService.GetAllAsync(cancellationToken);
            return Ok(ResponseModel<IList<HotelDto>>.Ok(hotels));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetHotelById(Guid id, CancellationToken cancellationToken = default)
        {
            var hotel=await _hotelService.GetByIdAsync(id,cancellationToken);
            return Ok(ResponseModel<HotelDto>.Ok(hotel));
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDto dto, CancellationToken cancellationToken = default)
        {
            var hotel = await _hotelService.AddAsync(dto,cancellationToken);
            return StatusCode(201, ResponseModel<HotelDto>.Created(hotel));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateHotel(Guid id, [FromBody] UpdateHotelDto dto, CancellationToken cancellationToken = default)
        {
            var hotel = await _hotelService.UpdateAsync(id, dto,cancellationToken);
            return Ok(ResponseModel<HotelDto>.Ok(hotel));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteHotel(Guid id, CancellationToken cancellationToken = default)
        {
            await _hotelService.DeleteAsync(id,cancellationToken);
            return Ok(ResponseModel<bool>.Ok(true));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterHotels([FromQuery] FilterHotelDto dto, CancellationToken cancellationToken = default)
        {
            var hotels = await _hotelService.GetFilteredAsync(dto,cancellationToken);
            return Ok(ResponseModel<IList<HotelDto>>.Ok(hotels));
        }
    }
}
