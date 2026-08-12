using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Common;

namespace TBS.Domain.Entitites
{
    public class City: AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public Guid CountryId { get; set; }
       
        public Country Country { get; set; } = null!;
        public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
        public ICollection<Airport> Airports { get; set; } = new List<Airport>();

    }
}
