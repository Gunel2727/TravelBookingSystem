using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Common;

namespace TBS.Domain.Entitites
{
    public class RoomImage: AuditableEntity
    {
        public string ImageUrl { get; set; } = string.Empty;
        public Guid RoomId { get; set; }
        public Room Room { get; set; } = null!;
    
    }
}
