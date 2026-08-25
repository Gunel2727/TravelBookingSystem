using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Hotel;

namespace TBS.Application.Interfaces
{
    public interface IHotelService
    {
        Task<HotelDto> GetByIdAsync(Guid id);
        Task<IList<HotelDto>> GetAllAsync();
        Task<HotelDto> AddAsync(CreateHotelDto dto);
        Task<HotelDto> UpdateAsync(Guid id, UpdateHotelDto dto);
        Task DeleteAsync(Guid id);
        Task<IList<HotelDto>> GetFilteredAsync(FilterHotelDto dto);
    }
}
