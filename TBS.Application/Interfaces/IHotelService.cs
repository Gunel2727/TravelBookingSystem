using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Common;
using TBS.Application.DTOs.Hotel;

namespace TBS.Application.Interfaces
{
    public interface IHotelService
    {
        Task<HotelDto> GetByIdAsync(Guid id,CancellationToken cancellationToken=default);
        Task<IList<HotelDto>> GetAllAsync(CancellationToken cancellationToken=default);
        Task<HotelDto> AddAsync(CreateHotelDto dto, CancellationToken cancellationToken = default);
        Task<HotelDto> UpdateAsync(Guid id, UpdateHotelDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<PagedList<HotelDto>> GetFilteredHotelsAsync(HotelFilterDto dto, CancellationToken cancellationToken = default);
    }
}
