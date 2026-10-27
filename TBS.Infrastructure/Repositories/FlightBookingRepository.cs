using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TBS.Domain.Entitites;
using TBS.Domain.Interfaces;
using TBS.Infrastructure.Data;

namespace TBS.Infrastructure.Repositories
{
    public class FlightBookingRepository : GenericRepository<FlightBooking>, IFlightBookingRepository
    {
        public FlightBookingRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<FlightBooking> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.FlightBookings
                .Include(fb => fb.ApplicationUser)
                .Include(fb => fb.Flight)
                .Include(fb => fb.FlightBookingPassengers)
                    .ThenInclude(fbp => fbp.Passenger)
                .Include(fb => fb.FlightBookingPassengers)
                    .ThenInclude(fbp => fbp.Flight) 
                .Include(fb => fb.Payments)
                .FirstOrDefaultAsync(fb => fb.Id == id, cancellationToken);
        }

        public override async Task<IList<FlightBooking>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.FlightBookings
                .AsNoTracking()
                .Include(fb => fb.ApplicationUser)
                .Include(fb => fb.Flight)
                .Include(fb => fb.FlightBookingPassengers)
                    .ThenInclude(fbp => fbp.Passenger)
                .Include(fb => fb.FlightBookingPassengers)
                    .ThenInclude(fbp => fbp.Flight) 
                .Include(fb => fb.Payments)
                .ToListAsync(cancellationToken);
        }

        public async Task<IList<FlightBooking>> GetBookingsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.FlightBookings
                .AsNoTracking()
                .Where(fb => fb.ApplicationUser.Id == userId)
                .Include(fb => fb.Flight)
                .Include(fb => fb.FlightBookingPassengers)
                    .ThenInclude(fbp => fbp.Passenger)
                .Include(fb => fb.FlightBookingPassengers)
                    .ThenInclude(fbp => fbp.Flight) 
                .Include(fb => fb.Payments)
                .ToListAsync(cancellationToken);
        }
    }
}