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

        public async Task<IList<Review>> GetReviewsByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default)
        {
            return await _context.Reviews
                .Include(r => r.ApplicationUser)
                .Where(r => r.HotelId == hotelId).ToListAsync(cancellationToken);
        }        
    }
}
