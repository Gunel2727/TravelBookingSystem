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
        public async Task<IActionResult> GetAllRooms(CancellationToken cancellationToken = default)
        {
            var rooms=await _roomService.GetAllAsync(cancellationToken);
            return Ok (ResponseModel<IList<RoomDto>>.Ok(rooms));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetRoomById(Guid id, CancellationToken cancellationToken = default)
        {
            var room=await _roomService.GetByIdAsync(id,cancellationToken);
            return Ok(ResponseModel<RoomDto>.Ok(room));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomDto dto, CancellationToken cancellationToken = default)
        {
            var room=await _roomService.AddAsync(dto,cancellationToken);
            return StatusCode(201, ResponseModel<RoomDto>.Created(room));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRoom(Guid id, CancellationToken cancellationToken = default) 
        { 
            await _roomService.DeleteAsync(id,cancellationToken);
            return Ok(ResponseModel<bool>.Ok(true));

        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRoom(Guid id, [FromBody] UpdateRoomDto dto, CancellationToken cancellationToken = default)
        {
            var room=await _roomService.UpdateAsync(id, dto,cancellationToken);
            return Ok(ResponseModel<RoomDto>.Ok(room));
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterRoom([FromQuery] FilterRoomDto dto, CancellationToken cancellationToken = default)
        {
            var rooms=await _roomService.GetFilteredAsync(dto,cancellationToken);
            return Ok(ResponseModel<IList<RoomDto>>.Ok(rooms));
        }
    }
}
