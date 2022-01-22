using Ipme.WikiBeer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Persistance.Repositories
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity, new()
    {
        private DbContext Context { get; } 

        public GenericRepository(DbContext context)
        {
            Context = context;
        }

        public virtual T Create(T entityToCreate)
        {
            T entity = Context.Add(entityToCreate).Entity;
            Context.SaveChanges(); 
            return entity;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public virtual T? GetById(Guid id)
        {
            return Context.Set<T>().SingleOrDefault(entity => entity.Id == id);
        }

        public virtual bool DeleteById(Guid id)
        {
            T? entity = GetById(id);
            if (entity == null)
                return false;

            Context.Set<T>().Remove(entity);
            
            return Context.SaveChanges() >= 1;
        }
    }
}
