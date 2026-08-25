using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBS.Application.DTOs.Room
{
    public class RoomDto
    {
        public Guid Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public string Name { get; set; }= string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal PriceForNight { get; set; }
        public int Capacity { get; set; }
        public string RoomType { get; set; } = string.Empty;
        public Guid HotelId { get; set; }
        public string HotelName { get; set;} = string.Empty;
        public ICollection<RoomImageDto> RoomImages { get; set; } = new List<RoomImageDto>();
    }
}
