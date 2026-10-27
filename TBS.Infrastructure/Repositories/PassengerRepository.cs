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
    public class PassengerRepository : GenericRepository<Passenger>, IPassengerRepository
    {
        public PassengerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Passenger?> GetByPassportNumberAsync(string passportNumber, CancellationToken cancellationToken = default)
        {
            return await _context.Passengers.FirstOrDefaultAsync(p => p.PassportNumber == passportNumber, cancellationToken);
        }
    }
}
