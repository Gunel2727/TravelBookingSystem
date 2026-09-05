using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Common;

namespace TBS.Domain.Entitites
{
    public class HotelBookingRoom: AuditableEntity
    {
        public Guid Id { get; set; }

        public decimal PriceForNight { get; set; }

        public Guid RoomId { get; set; }
        public Room Room { get; set; } = null!;

        public Guid HotelBookingId { get; set; }
        public HotelBooking HotelBooking { get; set; } = null!;

        public ICollection<HotelBookingRoomPassenger> HotelBookingRoomPassengers
        { get; set; } = new List<HotelBookingRoomPassenger>();


    }
}
