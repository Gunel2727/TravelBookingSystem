using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Entitites;

namespace TBS.Domain.Interfaces
{
    public interface IAirportRepository:IGenericRepository<Airport>
    {
        Task<Airport?> GetByIdWithCityAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> IsCodeUniqueAsync(string code,CancellationToken cancellationToken = default);
        Task<Airport?> GetWithFlightsAsync(Guid id, CancellationToken cancellationToken = default);
        
    }
}
