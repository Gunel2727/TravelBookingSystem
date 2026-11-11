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
        Task UploadImageAsync(UploadRoomImageDto uploadRoomImageDto);
    }
}
