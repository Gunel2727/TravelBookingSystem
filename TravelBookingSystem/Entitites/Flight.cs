using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Common;
using TBS.Domain.Enums;

namespace TBS.Domain.Entitites
{
    public class Flight: AuditableEntity
    {
        public string FlightNumber { get; set; } = string.Empty;
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public int StopCount { get; set; }
        public int AvailableSeats { get; set; }
        public Guid AirlineId { get; set; }
        public Airline Airline { get; set; } = null!;
        public Guid DepartureAirportId { get; set; }
        public Airport DepartureAirport { get; set; } = null!;
        public Guid ArrivalAirportId { get; set; }
        public Airport ArrivalAirport { get; set; } = null!;
        public FlightStatus Status { get; set; }

        public ICollection<FlightBookingPassenger> FlightBookingPassengers
        { get; set; } = new List<FlightBookingPassenger>();
    }
}
