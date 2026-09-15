using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Flight;

namespace TBS.Application.DTOs.Airport
{
    public class AirportDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; }= string.Empty;
        public Guid CityId { get; set; } 
        public string CityName { get; set; } = string.Empty;
    }
    public class CreateAirportDto
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public Guid CityId { get; set; }
    }
    public class UpdateAirportDto
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public Guid CityId { get; set; }
    }
}
