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
        Task<RoomDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IList<RoomDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<RoomDto> AddAsync(CreateRoomDto dto, CancellationToken cancellationToken = default);
        Task<RoomDto> UpdateAsync(Guid id,UpdateRoomDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IList<RoomDto>> GetFilteredAsync(FilterRoomDto dto, CancellationToken cancellationToken = default);
    }
}
