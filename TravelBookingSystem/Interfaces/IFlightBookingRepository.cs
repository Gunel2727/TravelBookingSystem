using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Entitites;

namespace TBS.Domain.Interfaces
{
    public interface IFlightBookingRepository:IGenericRepository<FlightBooking>
    {
        Task<IList<FlightBooking>> GetBookingsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
