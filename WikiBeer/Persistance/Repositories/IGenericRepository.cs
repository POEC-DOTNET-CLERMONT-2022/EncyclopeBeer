using Ipme.WikiBeer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Persistance.Repositories
{
    public interface IGenericRepository<T> where T : class, IEntity, new()
    {
        T Create(T entityToCreate);

        IEnumerable<T> GetAll();

        T? GetById(Guid id);

        T? UpdateById(Guid id, T entity);
        bool? DeleteById(Guid id);
    }
}
