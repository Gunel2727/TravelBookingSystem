using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Entitites;
using TBS.Domain.Interfaces;
using TBS.Infrastructure.Data;

namespace TBS.Infrastructure.Repositories
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IList<Room>> GetFilteredAsync(Guid? hotelId, decimal? minPrice, decimal? maxPrice, int? capacity)
        {
            var query = _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.RoomImages)
                .AsNoTracking()
                .AsQueryable();

            if(hotelId.HasValue)
            {
                query = query.Where(r => r.HotelId == hotelId.Value);
            }
            if (minPrice.HasValue)
            {
                query = query.Where(r => r.PriceForNight >= minPrice.Value);
            }
            if (maxPrice.HasValue) {
                query = query.Where(r => r.PriceForNight <= maxPrice.Value);

            }
            if (capacity.HasValue)
            {
                query = query.Where(r => r.Capacity >= capacity.Value);
            }
            return await query.ToListAsync();
        }
        public new async Task<Room?> GetByIdAsync(Guid id)
        {
            return await _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.RoomImages)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
        public new async Task<IList<Room>> GetAllAsync()
        {
            return await _context.Rooms
                .Include(r => r.Hotel)
                .Include(r => r.RoomImages)
                .ToListAsync();
        }
    }
}
