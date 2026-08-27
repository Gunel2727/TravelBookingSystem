using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Room;

namespace TBS.Application.Interfaces
{
    public interface IRoomService
    {
        Task<RoomDto> GetByIdAsync(Guid id);
        Task<IList<RoomDto>> GetAllAsync();
        Task<RoomDto> AddAsync(CreateRoomDto dto);
        Task<RoomDto> UpdateAsync(UpdateRoomDto dto,Guid id);
        Task DeleteAsync(Guid id);
        Task<IList<RoomDto>> GetFilteredAsync(FilterRoomDto dto);
    }
}
