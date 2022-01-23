using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perstistance
{
    public interface IGenericDbRepository<T> where T : class, IEntity, new()
    {
        //DbContext Context { get; }
        T Create(T entityToCreate);

        IList<T> GetAll();

        T? GetById(Guid id);

        bool DeleteById(Guid id);
    }
}
