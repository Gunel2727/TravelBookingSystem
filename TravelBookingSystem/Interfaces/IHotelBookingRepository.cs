using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Entitites;

namespace TBS.Domain.Interfaces
{
    public interface IHotelBookingRepository:IGenericRepository<HotelBooking>
    {
        Task<bool> IsRoomAvailableAsync(Guid roomId, DateTime checkInDate, DateTime checkOutDate, CancellationToken cancellationToken = default);
        Task<IList<HotelBooking>> GetBookingsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
