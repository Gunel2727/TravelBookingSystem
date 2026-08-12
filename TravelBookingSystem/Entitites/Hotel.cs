using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Common;

namespace TBS.Domain.Entitites
{
    public class Hotel:AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int StarRating { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid CityId { get; set; }
        public City City { get; set; } = null!;
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
        public ICollection<HotelImage> HotelImages { get; set; } = new List<HotelImage>();
        public ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();
    }
}
