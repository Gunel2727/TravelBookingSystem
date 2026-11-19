using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TBS.Application.Common;
using TBS.Application.DTOs.Room;
using TBS.Application.Interfaces;

namespace TBS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomImagesController : ControllerBase
    {
       private readonly IRoomImageService _roomImageService;

        public RoomImagesController(IRoomImageService roomImageService)
        {
            _roomImageService = roomImageService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] UploadRoomImageDto uploadRoomImageDto,CancellationToken cancellationToken = default)
        {
            var result = await _roomImageService.UploadImageAsync(uploadRoomImageDto, cancellationToken);
            return Ok(ResponseModel<RoomImageDto>.Ok(result));
        }

        [HttpGet("room/{roomId:guid}")]
        public async Task<IActionResult> GetImagesByRoomId(Guid roomId, CancellationToken cancellationToken = default)
        {
            var result = await _roomImageService.GetImagesByRoomIdAsync(roomId, cancellationToken);
            return Ok(ResponseModel<IList<RoomImageDto>>.Ok(result));
        }

        [HttpDelete("{imageId:guid}")]
        public async Task<IActionResult> DeleteImage(Guid imageId, CancellationToken cancellationToken = default)
        {
            await _roomImageService.DeleteAsync(imageId, cancellationToken);
            return Ok(ResponseModel<bool>.Ok(true));
        }
    }
}
