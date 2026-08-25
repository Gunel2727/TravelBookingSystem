using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBS.Application.DTOs.Hotel
{
    public class HotelImageDto
    {
        public Guid Id {  get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public Guid HotelId { get; set; }
        public string HotelName { get; set; }= string.Empty;
    }
}
