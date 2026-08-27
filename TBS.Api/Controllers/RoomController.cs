using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TBS.Application.Common;
using TBS.Application.DTOs.Room;
using TBS.Application.Interfaces;


namespace TBS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public  RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms=await _roomService.GetAllAsync();
            return Ok (ResponseModel<IList<RoomDto>>.Ok(rooms));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(Guid id)
        {
            var room=await _roomService.GetByIdAsync(id);
            return Ok(ResponseModel<RoomDto>.Ok(room));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomDto dto)
        {
            var room=await _roomService.AddAsync(dto);
            return StatusCode(201, ResponseModel<RoomDto>.Created(room));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(Guid id) 
        { 
            await _roomService.DeleteAsync(id);
            return Ok(ResponseModel<bool>.Ok(true));

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(Guid id, [FromBody] UpdateRoomDto dto)
        {
            var room=await _roomService.UpdateAsync(id, dto);
            return Ok(ResponseModel<RoomDto>.Ok(room));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterRoom([FromQuery] FilterRoomDto dto)
        {
            var rooms=await _roomService.GetFilteredAsync(dto);
            return Ok(ResponseModel<IList<RoomDto>>.Ok(rooms));
        }
    }
}
