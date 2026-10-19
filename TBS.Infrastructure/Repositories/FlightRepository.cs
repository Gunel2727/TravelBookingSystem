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
    public class FlightRepository : GenericRepository<Flight>, IFlightRepository
    {
        public FlightRepository(AppDbContext context) : base(context)
        {
        }

        

        public override async Task<Flight?> GetByIdAsync(Guid id,CancellationToken cancellationToken)
        {
            return await _context.Flights
                .Include(f => f.ArrivalAirport).ThenInclude(a => a.City)
                .Include(f => f.DepartureAirport).ThenInclude(a => a.City)
                .Include(f => f.Airline)
                .FirstOrDefaultAsync(f => f.Id == id,cancellationToken);
        }

        public override async Task<IList<Flight>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Flights
                .Include(f => f.ArrivalAirport).ThenInclude(a => a.City)
                .Include(f => f.DepartureAirport).ThenInclude(a => a.City)
                .Include(f => f.Airline)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<PagedList<Flight>> GetFilteredFlightsAsync(Guid? departureAirportId, Guid? arrivalAirportId, DateTime? departureDate, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var query = _context.Flights
                 .Include(f => f.ArrivalAirport)
                 .Include(f => f.DepartureAirport)
                 .Include(f => f.Airline)
                 .AsNoTracking()
                 .AsQueryable();

            if (departureAirportId.HasValue)
            {
                query = query.Where(f => f.DepartureAirportId == departureAirportId.Value);
            }

            if (arrivalAirportId.HasValue)
            {
                query = query.Where(f => f.ArrivalAirportId == arrivalAirportId.Value);
            }

            if (departureDate.HasValue)
            {
                var start = departureDate.Value.Date;
                var end = start.AddDays(1);

                query = query.Where(f =>
                    f.DepartureTime >= start &&
                    f.DepartureTime < end);
            }

            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);


            return new PagedList<Flight>(items, totalCount, pageNumber, pageSize);
        }
    }
}
