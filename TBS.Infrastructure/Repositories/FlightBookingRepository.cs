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
    public class FlightBookingRepository : GenericRepository<FlightBooking>, IFlightBookingRepository
    {
        public FlightBookingRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<FlightBooking> GetByIdAsync(Guid id)
        {
            return await _context.FlightBookings
                .Include(fb => fb.ApplicationUser)
                .Include(fb=>fb.Flight)
                .Include(fb => fb.FlightBookingPassengers)
                    .ThenInclude(fbp => fbp.Passenger)
                .Include(fb => fb.Payments)
                .FirstOrDefaultAsync(fb => fb.Id == id);
        }

        public override async Task<IList<FlightBooking>> GetAllAsync()
        {
            return await _context.FlightBookings
                .AsNoTracking()
                .Include(fb => fb.ApplicationUser)
                .Include(fb => fb.FlightBookingPassengers)
                .Include(fb => fb.Payments)
                .ToListAsync();
        }
    }
}
