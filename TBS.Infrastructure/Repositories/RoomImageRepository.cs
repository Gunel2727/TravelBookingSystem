using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Entitites;
using TBS.Domain.Interfaces;
using TBS.Infrastructure.Data;

namespace TBS.Infrastructure.Repositories
{
    public class RoomImageRepository : GenericRepository<RoomImage>, IRoomImageRepository
    {
        public RoomImageRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IList<RoomImage>> GetImagesByRoomIdAsync(Guid roomId, CancellationToken cancellationToken = default)
        {
           return await _context.RoomImages
                  .Where(ri => ri.RoomId == roomId)
                  .Include(ri => ri.Room)
                  .AsNoTracking()
                  .ToListAsync(cancellationToken);
        }
    }
}
