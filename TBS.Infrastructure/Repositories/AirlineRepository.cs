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
    public class AirlineRepository : GenericRepository<Airline>, IAirlineRepository
    {
        public AirlineRepository(AppDbContext context) : base(context)
        {
        }

        public async  Task<Airline?> GetWithFlightsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Airlines
                .Include(a => a.Flights)
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async  Task<bool> IsCodeUniqueAsync(string code, CancellationToken cancellationToken = default)
        {
            return  !await _context.Airlines.AnyAsync(a => a.Code == code, cancellationToken);
        }
    }
}
