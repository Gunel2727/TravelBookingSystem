using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Common;

namespace TBS.Domain.Entitites
{
    public class FlightBookingPassenger:AuditableEntity
    {
        public string SeatNumber { get; set; } = string.Empty;

        public Guid FlightBookingId { get; set; }
        public FlightBooking FlightBooking { get; set; } = null!;

        public Guid FlightId { get; set; }
        public Flight Flight { get; set; } = null!;

        public Guid PassengerId { get; set; }
        public Passenger Passenger { get; set; } = null!;

    }
}
