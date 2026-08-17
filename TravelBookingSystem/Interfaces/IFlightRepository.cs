using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Entitites;

namespace TBS.Domain.Interfaces
{
    public interface IFlightRepository : IGenericRepository<Flight>
    {
        Task<IList<Flight>> GetFilteredAsync(
            Guid? departureAirportId,
            Guid? arrivalAirportId,
            DateTime? departureDate);
    }
}
