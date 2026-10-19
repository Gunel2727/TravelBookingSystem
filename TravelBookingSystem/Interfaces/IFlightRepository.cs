using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Common;
using TBS.Domain.Entitites;

namespace TBS.Domain.Interfaces
{
    public interface IFlightRepository : IGenericRepository<Flight>
    {
        Task<PagedList<Flight>> GetFilteredFlightsAsync(
            Guid? departureAirportId,
            Guid? arrivalAirportId,
            DateTime? departureDate,
            int pageNumber,
            int pageSize
            , CancellationToken cancellationToken=default);
    }
}
