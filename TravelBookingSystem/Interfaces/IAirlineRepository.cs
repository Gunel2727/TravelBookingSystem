using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Entitites;

namespace TBS.Domain.Interfaces
{
    public interface IAirlineRepository : IGenericRepository<Airline>
    {
        Task<bool> IsCodeUniqueAsync(string code,CancellationToken cancellationToken = default);
        Task<Airline?> GetWithFlightsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
