using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Common;
using TBS.Domain.Enums;

namespace TBS.Domain.Entitites
{
    public class HotelBooking: AuditableEntity
    {
        public string ConfirmationNumber { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.UtcNow;
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public BookingStatus Status { get; set; }

        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!;

        public ICollection<HotelBookingRoom> HotelBookingRooms
        { get; set; } = new List<HotelBookingRoom>();

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
