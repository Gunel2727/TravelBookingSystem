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
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels= await _hotelService.GetAllAsync();
            return Ok(ResponseModel<IList<HotelDto>>.Ok(hotels));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(Guid id)
        {
            var hotel=await _hotelService.GetByIdAsync(id);
            return Ok(ResponseModel<HotelDto>.Ok(hotel));
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDto dto)
        {
            var hotel = await _hotelService.AddAsync(dto);
            return StatusCode(201, ResponseModel<HotelDto>.Created(hotel));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(Guid id, [FromBody] UpdateHotelDto dto)
        {
            var hotel = await _hotelService.UpdateAsync(id, dto);
            return Ok(ResponseModel<HotelDto>.Ok(hotel));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(Guid id)
        {
            await _hotelService.DeleteAsync(id);
            return Ok(ResponseModel<bool>.Ok(true));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterHotels([FromQuery] FilterHotelDto dto)
        {
            var hotels = await _hotelService.GetFilteredAsync(dto);
            return Ok(ResponseModel<IList<HotelDto>>.Ok(hotels));
        }
    }
}
