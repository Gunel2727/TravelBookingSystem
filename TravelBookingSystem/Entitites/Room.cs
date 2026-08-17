using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Common;
using TBS.Domain.Enums;

namespace TBS.Domain.Entitites
{
    public class Room:AuditableEntity
    {
        public string RoomNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty;
        public decimal PriceForNight { get; set; }
        public int Capacity { get; set; }
        public RoomType RoomType { get; set; } 
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }=null!;
        public ICollection<RoomImage> RoomImages { get; set; } = new List<RoomImage>();
    }
}
