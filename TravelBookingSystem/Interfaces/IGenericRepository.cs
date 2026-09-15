using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBS.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id,CancellationToken cancellationToken=default);
        Task<IList<T>> GetAllAsync(CancellationToken cancellationToken=default);

        Task AddAsync(T entity);

        void Update(T entity);
        void Delete(T entity);
    }
}
