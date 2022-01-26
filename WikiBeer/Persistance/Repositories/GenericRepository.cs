using Ipme.WikiBeer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ipme.WikiBeer.Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity, new()
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

        /// <summary>
        /// Update par remplacement (avec transfert de Guid)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual T? UpdateById(Guid id, T entity)
        {
            T? entityToUpdate = GetById(id);
            if (entityToUpdate == null)
                return null;

            entity.Id = entityToUpdate.Id;

            var updatedEntity = Context.Update(entity).Entity;// Faire un .Entity pourrait être une bonne pratique
                                                              // mais ici comme on fait un Get avant 
            Context.SaveChanges();

            return updatedEntity;
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
