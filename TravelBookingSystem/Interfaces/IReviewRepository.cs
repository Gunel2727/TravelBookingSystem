using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Entitites;

namespace TBS.Domain.Interfaces
{
    public interface IReviewRepository:IGenericRepository<Review>
    {
        Task<IList<Review>> GetReviewsByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default);
    }
}
