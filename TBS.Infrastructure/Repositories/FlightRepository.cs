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
    public class FlightRepository : GenericRepository<Flight>, IFlightRepository
    {
        public FlightRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IList<Flight>> GetFilteredAsync(Guid? departureAirportId, Guid? arrivalAirportId, DateTime? departureDate)
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
                query=query.Where(f=>f.ArrivalAirportId == arrivalAirportId.Value);
            }

            if (departureDate.HasValue)
            {
                var start = departureDate.Value.Date;
                var end = start.AddDays(1);

                query = query.Where(f =>
                    f.DepartureTime >= start &&
                    f.DepartureTime < end);
            }


            return await query.ToListAsync();
        }

        public new async Task<Flight?> GetByIdAsync(Guid id)
        {
            return await _context.Flights
                .Include(f => f.ArrivalAirport)
                .Include(f => f.DepartureAirport)
                .Include(f => f.Airline)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public new async Task<IList<Flight>> GetAllAsync()
        {
            return await _context.Flights
                .Include(f => f.ArrivalAirport)
                .Include(f => f.DepartureAirport)
                .Include(f => f.Airline)
                .AsNoTracking()
                .ToListAsync();
        }

    }
}
