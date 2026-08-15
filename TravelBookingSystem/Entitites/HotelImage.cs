using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Common;

namespace TBS.Domain.Entitites
{
    public class HotelImage:AuditableEntity
    {
        public string ImageUrl { get; set; } = string.Empty;

        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }=null!;
    }
}
