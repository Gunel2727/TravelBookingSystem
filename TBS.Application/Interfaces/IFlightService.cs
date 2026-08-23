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
        Task<FlightDto> GetByIdAsync(Guid id);
        Task<IList<FlightDto>> GetAllAsync();
        Task<FlightDto> AddAsync(CreateFlightDto dto);
        Task<FlightDto> UpdateAsync(Guid id, UpdateFlightDto dto);
        Task DeleteAsync(Guid id);
        Task<IList<FlightDto>> GetFilteredAsync(
            Guid? departureAirportId,
            Guid? arrivalAirportId,
            DateTime? departureDate);
    }
}
