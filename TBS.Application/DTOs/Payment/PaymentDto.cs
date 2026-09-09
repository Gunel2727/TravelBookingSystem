using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBS.Application.DTOs.Payment
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
        public string Provider { get; set; } = string.Empty;
        public string ProviderTransactionId { get; set; } = string.Empty;
    }
    public class CreatePaymentDto
    {
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
      
        public Guid? FlightBookingId { get; set; }
        public Guid? HotelBookingId { get; set; }
    }
    public class UpdatePaymentDto
    {
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;

        public Guid? FlightBookingId { get; set; }
        public Guid? HotelBookingId { get; set; }
    }

}
