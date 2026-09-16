using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Airport;

namespace TBS.Application.Interfaces
{
    public interface IAirportService
    {
        Task<AirportDto> GetByIdAsync(Guid id,CancellationToken cancellationToken=default);
        Task<IList<AirportDto>> GetAllAsync(CancellationToken cancellationToken=default);
        Task<AirportDto> CreateAsync(CreateAirportDto dto,CancellationToken cancellationToken = default);
        Task<AirportDto> UpdateAsync(Guid id, UpdateAirportDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        
    }
}
