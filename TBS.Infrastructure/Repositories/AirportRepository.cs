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
    public class AirportRepository : GenericRepository<Airport>, IAirportRepository
    {
        public AirportRepository(AppDbContext context) : base(context)
        {
        }

        public async  Task<Airport?> GetByIdWithCityAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Airports
                .Include(a => a.City)
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
            
        }

        public async  Task<Airport?> GetWithFlightsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Airports
                .Include(a => a.DepartureFlights)
                .Include(a => a.ArrivalFlights)
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async  Task<bool> IsCodeUniqueAsync(string code, CancellationToken cancellationToken = default)
        {
            return !await _context.Airports.AnyAsync(a => a.Code == code, cancellationToken);
        }
    }
}
