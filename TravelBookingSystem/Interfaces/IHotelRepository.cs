using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Entitites;

namespace TBS.Domain.Interfaces
{
    public interface IHotelRepository:IGenericRepository<Hotel>
    {
        Task<IList<Hotel>> GetFilteredAync(string? search, Guid? cityId);
        
    }
}
