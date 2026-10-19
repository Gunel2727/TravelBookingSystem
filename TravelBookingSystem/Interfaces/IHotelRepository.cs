using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Common;
using TBS.Domain.Entitites;

namespace TBS.Domain.Interfaces
{
    public interface IHotelRepository:IGenericRepository<Hotel>
    {
        Task<PagedList<Hotel>> GetFilteredHotelsAsync(
            string? searchTerm, 
            Guid? cityId,
            decimal? minPrice,
            decimal? maxPrice,
            int? rating,
            int pageNumber,
            int pageSize, CancellationToken cancellationToken=default);
        
    }
}
