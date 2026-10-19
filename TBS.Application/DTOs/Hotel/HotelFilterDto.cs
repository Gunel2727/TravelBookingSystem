using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Common;

namespace TBS.Application.DTOs.Hotel
{
    public class HotelFilterDto:PageRequestDto
    {
        public string? SearchTerm { get; set; }
        public Guid? CityId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? Rating { get; set; }
    }
}
