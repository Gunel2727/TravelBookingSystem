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
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<Review?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Reviews
                .Include(r => r.ApplicationUser)
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public override async Task<IList<Review>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Reviews
                .AsNoTracking()
                .Include(r => r.ApplicationUser)
                .Include(r => r.Hotel)
                .ToListAsync(cancellationToken);
        }

        public async Task<IList<Review>> GetReviewsByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default)
        {
            return await _context.Reviews
                .AsNoTracking()
                .Include(r => r.ApplicationUser)
                .Include(r => r.Hotel)
                .Where(r => r.HotelId == hotelId)
                .ToListAsync(cancellationToken);
        }
    }
}