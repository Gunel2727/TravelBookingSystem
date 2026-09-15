using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Airline;

namespace TBS.Application.Interfaces
{
    public interface IAirlineService
    {
        Task<AirlineDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IList<AirlineDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<AirlineDto> CreateAsync(CreateAirlineDto dto, CancellationToken cancellationToken = default);
        Task<AirlineDto> UpdateAsync(Guid id, UpdateAirlineDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
