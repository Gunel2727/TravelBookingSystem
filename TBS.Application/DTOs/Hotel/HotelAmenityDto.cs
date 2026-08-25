using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBS.Application.DTOs.Hotel
{
    public class HotelAmenityDto
    {
        public Guid HotelId { get; set; }
        public string HotelName { get; set; } = string.Empty;
        public string AmenityName { get; set; }= string.Empty;
        public Guid AmenityId { get; set; } 

    }
}
