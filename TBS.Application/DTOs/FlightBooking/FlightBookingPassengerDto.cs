using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Entitites;

namespace TBS.Application.DTOs.FlightBooking
{
    public class FlightBookingPassengerDto
    {
        public string SeatNumber { get; set; } = string.Empty;

        public Guid FlightBookingId { get; set; }
        public string FlightBookingName { get; set; } = null!;

        public Guid FlightId { get; set; }
        public string FlightName { get; set; } = null!;

        public Guid PassengerId { get; set; }
        public string PassengerName { get; set; } = null!;
    }
}
