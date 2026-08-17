using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Common;
using TBS.Domain.Enums;

namespace TBS.Domain.Entitites
{
    public class FlightBooking:AuditableEntity
    {
        public string ConfirmationNumber { get; set; } = string.Empty;
        public string FlightBookingNumber { get; set; }=string.Empty;
        public DateTime BookingDate { get; set; }
        public BookingStatus Status { get; set; }
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!;

        public ICollection<FlightBookingPassenger> FlightBookingPassengers
        { get; set; } = new List<FlightBookingPassenger>();

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
