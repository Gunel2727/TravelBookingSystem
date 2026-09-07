using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.HotelBooking;

namespace TBS.Application.Interfaces
{
    public interface IHotelBookingService
    {
        Task<HotelBookingDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IList<HotelBookingDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<HotelBookingDto> AddAsync(CreateHotelBookingDto dto, CancellationToken cancellationToken = default);
        Task<HotelBookingDto> UpdateAsync(Guid id,UpdateHotelBookingDto dto, CancellationToken cancellationToken = default);
        Task CancelBookingAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> IsRoomAvailableAsync(Guid roomId, DateTime checkInDate, DateTime checkOutDate,CancellationToken cancellationToken = default);
    }
}
