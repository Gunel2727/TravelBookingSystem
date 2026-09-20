using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Flight;

namespace TBS.Application.Interfaces
{
    public interface IFlightService
    {
        Task<FlightDto> GetByIdAsync(Guid id,CancellationToken cancellationToken=default);
        Task<IList<FlightDto>> GetAllAsync(CancellationToken cancellationToken=default);
        Task<FlightDto> AddAsync(CreateFlightDto dto,CancellationToken cancellationToken=default);
        Task<FlightDto> UpdateAsync(Guid id, UpdateFlightDto dto,CancellationToken cancellationToken=default);
        Task DeleteAsync(Guid id,CancellationToken cancellationToken=default);
        Task<IList<FlightDto>> GetFilteredAsync(FilterFlightDto dto,CancellationToken cancellationToken=default);
    }
}
