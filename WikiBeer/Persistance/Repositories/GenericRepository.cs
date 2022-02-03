using Ipme.WikiBeer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// A vérifier : de l'intrêt du repository : (est-ce une bonne idée???)
/// voir : https://rob.conery.io/2014/03/04/repositories-and-unitofwork-are-not-a-good-idea/
/// Sur une alternative pour les CRUD : 
/// voir : https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/crud?view=aspnetcore-6.0#alternative-httppost-edit-code-create-and-attach
/// TODO : vérifier si c'est une bonne idée de passer par du générique.
/// Cela force nos entities à avoir un constructeur public si on veut pouvoir en instancier de 
/// nouvelles ici!
/// TODO voir les Activator : 
/// voir : https://docs.microsoft.com/en-us/dotnet/api/system.activator?view=net-6.0
/// TODO : enelver définitivement le new et compléter IEntity par une BaseEntity (avec constructeur internal)
/// </summary>
namespace Ipme.WikiBeer.Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity//, new()
    {
        private DbContext Context { get; } 

        public GenericRepository(DbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// TODO : check les id des contenues, s'occuper de ceux ci (vérifier leur existance ou non [via Get] pour les 
        /// ramener dans le Context) avant de s'occuper avant de s'occuper du contenant puis en dernier faire le Add sur le contenant.
        /// </summary>
        /// <param name="entityToCreate"></param>
        /// <returns></returns>
        public virtual T Create(T entityToCreate)
        {
            //b18de927-2251-4c2b-198d-08d9e560afa1
            //T entity = Context.Set<T>().Add(entityToCreate).Entity;
            T entity = Context.Attach(entityToCreate).Entity;
            Context.SaveChanges(); 
            return entity;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        /// <summary>
        /// TODO : remplacer définitivement par un find (qui renvoie également un null si non trouvé
        /// mais pas si il en trouve plusieurs)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T? GetById(Guid id)
        {
            return Context.Set<T>().Find(id);
            //return Context.Set<T>().SingleOrDefault(entity => entity.Id == id);
        }

        /// <summary>
        /// Update par remplacement (avec transfert de Guid)
        /// TODO : à remplacer par un update simple
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual T? UpdateById(Guid id, T entity)
        {
            T? entityToUpdate = GetById(id);
            if (entityToUpdate == null)
                return null;

            
            //var tt = new T() { Id = id };
            entity.Id = entityToUpdate.Id;

            var updatedEntity = Context.Update(entity).Entity;// Faire un .Entity pourrait être une bonne pratique
                                                              // mais ici comme on fait un Get avant 
            Context.SaveChanges();

            return updatedEntity;
        }

        /// <summary>
        /// TODO à revoir pour ne faire q'un seul appel à la bdd (voir activator)
        /// Pourquoi ne pas juste lui passer une entité au lieu d'un id?
        ///  -> pasque c'est la merde coté controller!
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool? DeleteById(Guid id)
        {
            T? entity = GetById(id);
            if (entity == null)
                return null;
            // Il faut passer par un activator pour faire ce genre de chose
            //var entity = new { Id = id }; // ne fonctionne que sur le fait que l'on a un setter public... c'est moche
            //var entity2 = (T)Activator.CreateInstance(typeof(T), id);
            Context.Set<T>().Attach(entity);
            Context.Set<T>().Remove(entity); // on peut enlever le Set<T> ici...
            
            return Context.SaveChanges() >= 1;
        }
    }
}
