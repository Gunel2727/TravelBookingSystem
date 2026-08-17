using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Common;

namespace TBS.Domain.Entitites
{
    public class Review:AuditableEntity
    {
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }=null!;

        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;
    }
}
