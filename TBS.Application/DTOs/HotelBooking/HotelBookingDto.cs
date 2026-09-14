using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Payment;
using TBS.Domain.Entitites;
using TBS.Domain.Enums;

namespace TBS.Application.DTOs.HotelBooking
{
    public class HotelBookingDto
    {
        public Guid Id { get; set; }
        public string ConfirmationNumber { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public Guid ApplicationUserId { get; set; }
        public decimal TotalPrice { get; set; }
        public string ApplicationUserName { get; set; }= string.Empty;
        public ICollection<HotelBookingRoomDto> HotelBookingRooms
        { get; set; } = new List<HotelBookingRoomDto>();

        public ICollection<PaymentDto> Payments { get; set; } = new List<PaymentDto>();
    }
    public class CreateHotelBookingDto
    {
       
       public List<Guid> RoomIds { get; set; } = new List<Guid>();
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
      
        public Guid ApplicationUserId { get; set; }

    }
    public class UpdateHotelBookingDto
    {
       

        public List<Guid> RoomIds { get; set; } = new List<Guid>();
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public Guid ApplicationUserId { get; set; }

    }
}
