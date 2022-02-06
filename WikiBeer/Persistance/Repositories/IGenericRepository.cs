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
        T Create(T entityToCreate);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetAllNoInclude();

        T? GetById(Guid id);

        T? GetByIdNoInclude(Guid id);

        T? Update(T entity);

        bool? DeleteById(Guid id);
    }
}
