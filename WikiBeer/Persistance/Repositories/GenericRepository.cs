using Ipme.WikiBeer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
/// Note importante : On fait le choix de la sécurité sur les Update, Create. On vérifie systématiquement l'existence des objets en base  
/// TODO : revoir les vérification qui au final ne servent à rien (passer plutôt par GetDatabaseValue ou bien via Any)
/// https://docs.microsoft.com/en-us/ef/core/change-tracking/entity-entries
/// https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.changetracking.entityentry.getdatabasevalues?view=efcore-6.0#microsoft-entityframeworkcore-changetracking-entityentry-getdatabasevalues
/// Sur l'implémentation d'une searchbar (expression-tree qu'il va falloir associer à de la réflexion!): 
/// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/expression-trees/how-to-use-expression-trees-to-build-dynamic-queries
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

        public virtual async Task<T?> CreateAsync(T entityToCreate)
        {
            if (entityToCreate.Id != Guid.Empty)
                return null; 

            var newEntry = Context.Attach(entityToCreate);

            //CheckBorderEffectAdded(entityToCreate);

            await Context.SaveChangesAsync(); 
            return newEntry.Entity;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllNoIncludeAsync()
        {
            return await Context.Set<T>().IgnoreAutoIncludes().ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {            
            return await Context.Set<T>().FindAsync(id);       
        }

        public virtual async Task<T?> GetByIdNoIncludeAsync(Guid id)
        {
            return await Context.Set<T>().IgnoreAutoIncludes().SingleOrDefaultAsync(obj => obj.Id == id);
        }

        public virtual async Task<T?> UpdateAsync(T entity)
        {
            var updatedEntry = Context.Attach(entity);
            if (!Context.Set<T>().Any(e => e.Id == entity.Id))
                return null;
  
            updatedEntry.State = EntityState.Modified;

            CheckBorderEffectAdded(entity);

            await Context.SaveChangesAsync();            
            return updatedEntry.Entity;
        }

        /// <summary>
        /// TODO à revoir pour ne faire q'un seul appel à la bdd (voir activator)
        /// Pourquoi ne pas juste lui passer une entité au lieu d'un id?
        ///  -> pasque c'est la merde coté controller!
        /// En fait on est obligé de faire un getById car on doit récupérer les
        /// dépendances pour faire la suppression propre des relations optionnelles
        /// (sauf dans le cas de la récupération d'un dépendant au sens EFCore : pour l'instant Beer
        /// uniquement)
        /// https://stackoverflow.com/questions/49593482/entity-framework-core-2-0-1-eager-loading-on-all-nested-related-entities/49597502#49597502
        /// Mais en fait non... revoir pour un activator?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<bool?> DeleteByIdAsync(Guid id)
        {
            T? entity = await GetByIdAsync(id);
            if (entity == null)
                return null;

            Context.Remove(entity);

            return await Context.SaveChangesAsync() >= 1;
        }

        /// <summary>
        /// TODO : à retravailler pour tester aussi la modification par rapport à la base!
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="UndesiredBorderEffectException"></exception>
        private void CheckBorderEffectAdded(T entity)
        {
            var entries = Context.ChangeTracker.Entries().Where(e => e.Entity != entity && e.Entity is not Dictionary<string, object>);

            if (entries.Any())
            {
                foreach (var entry in entries)
                {
                    if (entry.State == EntityState.Added)
                        throw new UndesiredBorderEffectException("L'ajout en base d'un composant lors de création/modification" +
                            $"d'un composé n'est pas autorisée. (composé :{entity.Id} : {entity})");
                }
            }
        }


    }
}
