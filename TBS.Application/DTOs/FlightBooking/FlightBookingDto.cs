using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Payment;
using TBS.Domain.Entitites;

namespace TBS.Application.DTOs.FlightBooking
{
    public class FlightBookingDto
    {
        public Guid Id { get; set; }
        public string ConfirmationNumber { get; set; } = string.Empty;
        public string FlightBookingNumber { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public Guid ApplicationUserId { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<FlightBookingPassengerDto> FlightBookingPassengers
        { get; set; } = new List<FlightBookingPassengerDto>();

        public ICollection<PaymentDto> Payments { get; set; } = new List<PaymentDto>();
    }
    public class CreateFlightBookingDto
    {
        public Guid FlightId { get; set; }
        public List<Guid> PassengerIds { get; set; } = new List<Guid>();
       
    }
    public class UpdateFlightBookingDto 
    {
        public List<Guid> PassengerIds { get; set; } = new List<Guid>();
        
    }
}
