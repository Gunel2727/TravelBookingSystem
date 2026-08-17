using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Common;
using TBS.Domain.Enums;

namespace TBS.Domain.Entitites
{
    public class Payment : AuditableEntity
    {
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Currency Currency { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime PaymentDate { get; set; }

        public string Provider { get; set; } = string.Empty;
        public string ProviderTransactionId { get; set; } = string.Empty;

        public Guid? FlightBookingId { get; set; }
        public FlightBooking? FlightBooking { get; set; }

        public Guid? HotelBookingId { get; set; }
        public HotelBooking? HotelBooking { get; set; }
    }
}
