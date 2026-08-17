using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Common;

namespace TBS.Domain.Entitites
{
    public class HotelBookingRoomPassenger : AuditableEntity
    {
        public Guid HotelBookingRoomId { get; set; }
        public HotelBookingRoom HotelBookingRoom { get; set; } = null!;

        public Guid PassengerId { get; set; }
        public Passenger Passenger { get; set; } = null!;
    }
}
