using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perstistance.Repositories
{
    public class GenericDbRepository<T> : IGenericDbRepository<T> where T : class, IEntity, new()
    {
        public DbContext Context { get; } // public pour pouvoir être appelé dans un controller (et satisfaire à l'interface)

        public GenericDbRepository(DbContext context)
        {
            Context = context;
        }

        public virtual T Create(T entityToCreate)
        {
            T entity = Context.Add(entityToCreate).Entity;
            Context.SaveChanges(); // Enregistre les changements en base
            return entity;
        }

        public virtual IList<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }


        //public IEnumerable<T> GetIAll()
        //{
        //    return Context.Set<T>();
        //}

        /// <summary>
        /// TODO : rajouter une exception sur le trhow possible du SingleOrDefault
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

            //SaveChanges() retourne le nombre de ligne modifiée
            return Context.SaveChanges() >= 1;
        }  
    }
}
