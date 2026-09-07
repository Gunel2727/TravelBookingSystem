using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBS.Domain.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IFlightRepository FlightRepository { get; }
        IHotelRepository HotelRepository { get; }
        IRoomRepository RoomRepository { get; }
        IHotelBookingRepository HotelBookingRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
