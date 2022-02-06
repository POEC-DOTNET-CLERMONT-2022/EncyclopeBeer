using Ipme.WikiBeer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
/// Pokur régler le problème de Delete (qui ne fonctionne pas car le Graph de l'objet
/// entier n'est pas chargé)
/// https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.changetracking.changetracker.trackgraph?view=efcore-6.0
/// Note importante : On fait le choix de la sécurité sur les Update, Delete. On vérifie systématiquement l'existence des objest en base  
/// </summary>
namespace Ipme.WikiBeer.Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private DbContext Context { get; } 

        public GenericRepository(DbContext context)
        {
            Context = context;
        }

        public virtual T Create(T entityToCreate)
        {
            var newEntry = Context.Attach(entityToCreate);
            
            var entries = Context.ChangeTracker.Entries().ToList();
            entries.Remove(newEntry);
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                    throw new UndesiredBorderEffectException("La modification ou l'ajout d'un composant lors de création" +
                        $"d'un composé n'est pas authorisée. (composé : {entityToCreate})"); 
            }

            Context.SaveChanges(); 
            return newEntry.Entity;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public virtual T? GetById(Guid id)
        {
            return Context.Set<T>().Find(id);       
        }

        public virtual T? Update(T entity)
        {
            var updatedEntry = Context.Attach(entity); 
            if (updatedEntry.State == EntityState.Added)
                return null; // ressource non trouvé car marqué Added

            updatedEntry.State = EntityState.Modified; // met entity et ses composants dans l'état modifié

            // Pour test des effets de bords
            var entries = Context.ChangeTracker.Entries().ToList();
            entries.Remove(updatedEntry);
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                    throw new UndesiredBorderEffectException($"La modification ou l'ajout d'un composant lors de la modification " +
                        $"d'un composé n'est pas authorisée. (composé : {entity})"); // modifications non voulue
            }
            Context.SaveChanges();            
            return updatedEntry.Entity;
        }

        /// <summary>
        /// TODO à revoir pour ne faire q'un seul appel à la bdd (voir activator)
        /// Pourquoi ne pas juste lui passer une entité au lieu d'un id?
        ///  -> pasque c'est la merde coté controller!
        /// En fait on est obligé de faire un getById car on doit récupérer les
        /// dépendances pour faire la suppression propre des relations optionnelles
        /// (sauf dans le cas de la récupération d'un dépendant au sens EFCore)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool? DeleteById(Guid id)
        {
            T? entity = GetById(id);
            if (entity == null)
                return null;
  
            Context.Remove(entity);

            return Context.SaveChanges() >= 1;
        }
    }
}
