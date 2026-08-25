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
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        public HotelRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IList<Hotel>> GetFilteredAsync(string? search, Guid? cityId)
        {
            var query = _context.Hotels
                .Include(h => h.City)
                .Include(h=>h.HotelImages)
                .AsNoTracking()
                .AsQueryable();

            if(!string.IsNullOrWhiteSpace(search))
            {
                var searchTerm = search.ToLower();
                query = query.Where(h =>
                    h.Name.ToLower().Contains(searchTerm) ||
                    h.Description.ToLower().Contains(searchTerm));
            }

            if (cityId.HasValue)
            {
                query = query.Where(h => h.CityId == cityId.Value);
            }

            return await query.ToListAsync();
        }
        public new async Task<Hotel?> GetByIdAsync(Guid id)
        {
            return await _context.Hotels
                .Include(h => h.City)
                .Include(h => h.Rooms)
                .Include(h => h.HotelImages)
                .Include(h => h.HotelAmenities)
                .Include(h => h.Reviews)
                .FirstOrDefaultAsync(h => h.Id == id);
        }
        public new async Task<IList<Hotel>> GetAllAsync()
        {
            return await _context.Hotels
                .Include(h => h.City)
                .Include(h => h.HotelImages)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
