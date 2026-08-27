using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Review;
using TBS.Application.DTOs.Room;

namespace TBS.Application.DTOs.Hotel
{
    public class HotelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty;
        public string Address { get; set; } = string.Empty;
        public int StarRating { get; set; }
        public Guid CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
        public ICollection<RoomDto> Rooms { get; set; } = new List<RoomDto>();
        public ICollection<HotelImageDto> HotelImages { get; set; } = new List<HotelImageDto>();
        public ICollection<HotelAmenityDto> HotelAmenities { get; set; } = new List<HotelAmenityDto>();
        public ICollection<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();

    }
    public class CreateHotelDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int StarRating { get; set; }
        public Guid CityId { get; set; }
    }
    public class UpdateHotelDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int StarRating { get; set; }
        public Guid CityId { get; set; }
    }
    public class FilterHotelDto
    {
        public string? Search { get; set; }
        public Guid? CityId { get; set; }
    }
}
