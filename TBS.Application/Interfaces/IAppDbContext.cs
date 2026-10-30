using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TBS.Domain.Entitites;

namespace TBS.Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<RefreshToken> RefreshTokens { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}