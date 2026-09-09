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
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<Payment?> GetByIdAsync(Guid id)
        {
            return await _context.Payments
                .Include(p => p.FlightBooking)
                .Include(p => p.HotelBooking)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<IList<Payment>> GetAllAsync()
        {
            return await _context.Payments
                .AsNoTracking()
                .Include(p => p.FlightBooking)
                .Include(p => p.HotelBooking)
                .ToListAsync();
        }
    }
}
