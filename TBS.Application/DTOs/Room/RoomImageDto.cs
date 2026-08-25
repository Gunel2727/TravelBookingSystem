using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBS.Application.DTOs.Room
{
    public class RoomImageDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public Guid RoomId { get; set; }
        public string RoomName { get; set; } = string.Empty;
    }
}
