using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Entitites;

namespace TBS.Domain.Interfaces
{
    public interface IPassengerRepository : IGenericRepository<Passenger>
    {
        Task<Passenger?> GetByPassportNumberAsync(string passportNumber,CancellationToken cancellationToken=default);
    }
}
