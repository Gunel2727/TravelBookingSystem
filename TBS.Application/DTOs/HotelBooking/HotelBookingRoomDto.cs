using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Entitites;

namespace TBS.Application.DTOs.HotelBooking
{
    public class HotelBookingRoomDto
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public decimal PriceForNight { get; set; }

    }
}
