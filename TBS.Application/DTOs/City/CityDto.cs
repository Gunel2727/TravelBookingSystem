using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBS.Application.DTOs.City
{
    public class CityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public Guid CountryId { get; set; }
    }
}
