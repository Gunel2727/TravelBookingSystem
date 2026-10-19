using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Common;
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

       
        public override async Task<Hotel?> GetByIdAsync(Guid id,CancellationToken cancellationToken)
        {
            return await _context.Hotels
                .Include(h => h.City)
                .Include(h => h.Rooms)
                .Include(h => h.HotelImages)
                .Include(h => h.HotelAmenities)
                .Include(h => h.Reviews)
                .FirstOrDefaultAsync(h => h.Id == id,cancellationToken);
        }
        public override async Task<IList<Hotel>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Hotels
                .Include(h => h.City)
                .Include(h => h.HotelImages)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<PagedList<Hotel>> GetFilteredHotelsAsync(string? searchTerm, Guid? cityId, decimal? minPrice, decimal? maxPrice, int? rating, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var query = _context.Hotels
                .Include(h => h.City)
                .Include(h => h.HotelImages)
                .Include(h => h.Rooms)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query=query.Where(h => h.Name.Contains(searchTerm) || h.Description.Contains(searchTerm));
            }

            if(cityId.HasValue)
            {
                query=query.Where(h=>h.CityId == cityId.Value);
            }
            if(minPrice.HasValue)
            {
                query = query.Where(h => h.Rooms.Any(r => r.PriceForNight >= minPrice.Value));
            }
            if(maxPrice.HasValue)
            {
                query = query.Where(h => h.Rooms.Any(r => r.PriceForNight <= maxPrice.Value));
            }
            if(rating.HasValue)
            {
                query = query.Where(h => h.StarRating == rating.Value);
            }

            var totalCount=await query.CountAsync(cancellationToken);
            var items=await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return new PagedList<Hotel>(items, totalCount, pageNumber, pageSize);
        }
    }
}
