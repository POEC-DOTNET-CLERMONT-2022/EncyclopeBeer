using Ipme.WikiBeer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Persistance.Repositories
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        Task<T?> CreateAsync(T entityToCreate);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllNoIncludeAsync();

        Task<T?> GetByIdAsync(Guid id);

        Task<T?> GetByIdNoIncludeAsync(Guid id);

        Task<T?> UpdateAsync(T entity);

        Task<bool?> DeleteByIdAsync(Guid id);
    }
}
