using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Entitites;

namespace TBS.Domain.Interfaces
{
    public interface IRoomRepository:IGenericRepository<Room>
    {
        Task<IList<Room>> GetFilteredAsync(Guid? hotelId, decimal? minPrice, decimal? maxPrice, int? capacity,CancellationToken cancellationToken=default);
    }
}
