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
        private IRoomRepository _roomRepository;
        private IHotelBookingRepository _hotelBookingRepository;
        private IPaymentRepository _paymentRepository;
        private IFlightBookingRepository _flightBookingRepository;
        private IAirportRepository _airportRepository;
        private IAirlineRepository _airlineRepository;

        public IFlightRepository FlightRepository =>
            _flightRepository ??= new FlightRepository(_context);

        public IHotelRepository HotelRepository =>
            _hotelRepository ??= new HotelRepository(_context);

        public IRoomRepository RoomRepository => 
            _roomRepository ??= new RoomRepository(_context);

        public IHotelBookingRepository HotelBookingRepository => 
            _hotelBookingRepository ??= new HotelBookingRepository(_context);

        public IPaymentRepository PaymentRepository =>
            _paymentRepository ??= new PaymentRepository(_context);

        public IFlightBookingRepository FlightBookingRepository => 
            _flightBookingRepository ??= new FlightBookingRepository(_context);

        public IAirportRepository AirportRepository =>
            _airportRepository ??= new AirportRepository(_context);

        public IAirlineRepository AirlineRepository => 
            _airlineRepository ??= new AirlineRepository(_context);

        public void Dispose()
        {
           _context.Dispose();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
