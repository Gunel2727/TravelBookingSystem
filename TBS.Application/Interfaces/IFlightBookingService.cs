using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.FlightBooking;

namespace TBS.Application.Interfaces
{
    public interface IFlightBookingService
    {
        Task<FlightBookingDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IList<FlightBookingDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<FlightBookingDto> AddAsync(CreateFlightBookingDto dto, CancellationToken cancellationToken = default);
        Task<FlightBookingDto> UpdateAsync(Guid id, UpdateFlightBookingDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    }
}
