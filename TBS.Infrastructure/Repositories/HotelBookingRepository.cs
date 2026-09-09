using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Entitites;
using TBS.Domain.Enums;
using TBS.Domain.Interfaces;
using TBS.Infrastructure.Data;

namespace TBS.Infrastructure.Repositories
{
    public class HotelBookingRepository : GenericRepository<HotelBooking>, IHotelBookingRepository
    {
        public HotelBookingRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> IsRoomAvailableAsync(Guid roomId, DateTime checkInDate, DateTime checkOutDate, CancellationToken cancellationToken = default)
        {
            bool hasConflict = await _context.HotelBookings.AnyAsync(b =>
                 b.Status != BookingStatus.Cancelled &&
                 b.HotelBookingRooms.Any(r => r.RoomId == roomId) &&
                 checkOutDate > b.CheckInDate && checkInDate < b.CheckOutDate,
                 cancellationToken);

            return !hasConflict;
        }

        public override async Task<HotelBooking?> GetByIdAsync(Guid id)
        {
            return await _context.HotelBookings
                .Include(b => b.HotelBookingRooms)
                .ThenInclude(br => br.Room)
                .Include(b => b.Payments)
                .Include(b => b.ApplicationUser)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public override async Task<IList<HotelBooking>> GetAllAsync()
        {
            return await _context.HotelBookings
                .AsNoTracking()
                .Include(b => b.HotelBookingRooms)
                .ThenInclude(br => br.Room)
                
                .Include(b => b.ApplicationUser)
                .ToListAsync();
        }
    }
}
