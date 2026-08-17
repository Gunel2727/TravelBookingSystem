using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBS.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IList<T>> GetAllAsync();

        void AddAsync(T entity);

        void Update(T entity);
        void Delete(T entity);
    }
}
