using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Interfaces;
using TBS.Infrastructure.Data;

namespace TBS.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        private IHotelRepository _hotelRepository;
        private IFlightRepository _flightRepository; 

        public IFlightRepository FlightRepository =>
            _flightRepository ??= new FlightRepository(_context);

        public IHotelRepository HotelRepository =>
            _hotelRepository ??= new HotelRepository(_context);

        public void Dispose()
        {
           _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
