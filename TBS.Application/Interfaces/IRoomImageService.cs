using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Room;

namespace TBS.Application.Interfaces
{
    public interface IRoomImageService
    {
        Task<RoomImageDto> UploadImageAsync(UploadRoomImageDto uploadRoomImageDto,CancellationToken cancellationToken=default);
        Task<IList<RoomImageDto>> GetImagesByRoomIdAsync(Guid roomId,CancellationToken cancellationToken=default);
        Task DeleteAsync(Guid imageId, CancellationToken cancellationToken = default);
    }
}
