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
            return await Context.Set<T>().IgnoreAutoIncludes().FirstOrDefaultAsync(obj => obj.Id == id);
        }

        /// <summary>
        /// Problème à régler ici. Update ne fonctionne pas avec les tables d'associations (pète une erreur).
        /// Attach fonctionne mais pas correctement, les tables d'associations ne sont pas mise à jour.
        /// Idée : custom la relation au niveau du DbContext ou bien revoir l'architecture des dto 
        /// (comme alex et johan).
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        //public virtual async Task<T?> UpdateAsync(T entity)
        //{
        //    //var entry = Context.Entry(entity);
        //    //CustomTracking(entity);

        //    //entry.State = EntityState.Modified;
        //    var updatedEntry = Context.Attach(entity);
        //    //var values = updatedEntry.GetDatabaseValues();
        //    //var updatedEntry = Context.Update(entity); // mais passe tt en Modified (grosse requête en base) -> et les relations
        //    // intermédiaires passent en add quand il ne faut pas
        //    if (!Context.Set<T>().Any(e => e.Id == entity.Id))
        //        return null;

        //    //entry.State = EntityState.Modified;
        //    updatedEntry.State = EntityState.Modified;
        //    var fullEntrie = Context.ChangeTracker.Entries();
        //    //var entries = Context.ChangeTracker.Entries().Where(e => e.Entity == entity || e.Entity is Dictionary<string, object>);
        //    //var compiletype = entries.ToList()[1].
        //    //SetEntriesState(entries, EntityState.Modified);
        //    //SetStateExceptSelfAndCollections(entity, EntityState.Unchanged); // est sensé limiter le nombre de modif en base.
        //    //CheckBorderEffectAdded(entity);

        //    await Context.SaveChangesAsync();
        //    //var updatedEntry = Context.Attach(entity);
        //    return updatedEntry.Entity;
        //    //return entry.Entity;
        //}

        /// Sur comment récupérer une clef composite
        /// https://stackoverflow.com/questions/30688909/how-to-get-primary-key-value-with-entity-framework-core
        /// 
        public virtual async Task<T?> UpdateAsync(T entity)
        {
            var updatedEntry = Context.Attach(entity);

            if (!Context.Set<T>().Any(e => e.Id == entity.Id))
                return null;

            updatedEntry.State = EntityState.Modified;
            var fullEntrie = Context.ChangeTracker.Entries();
            var entries = Context.ChangeTracker.Entries().Where(e => e.Entity is UserBeer);

            // Des test d'un algo moins bourrins qu'ef core
            var keyNames = Context.Model.FindEntityType(typeof(UserBeer)).FindPrimaryKey()
             .Properties.Select(x => x.Name).ToList();
            var ent = entries.ToList()[0].Entity; // avec un cast sur un IAssociationTable qui force l'implem d'un get Tuple<Guid,Guid>
            var tt1 = ent.GetType(); // Type runtime
            var tt2 = tt1.GetProperties(); // Propriété associée
            var tt3 = tt2.Where(p => keyNames.Any(p.Name.Contains)); // Enumerable des Id de la clef composite
            //var method = tt1.Get; 
            //var key1 = ent.GetType().GetProperty(keyNames[0]).GetValue(ent, null);
            // Il nous faut une interface ET une classe Parent pour implémenter la méthode
            // Il faut en plus une méthode qui prend un id donné et check si l'un ou l'autre des PK/FK correspond
            var associationTableRows = Context.Set<UserBeer>().AsNoTracking().Where(atr => atr.IsInCompositeKey(entity.Id))
                .Select(atr => atr.GetCompositeKey); // ce truc doit renvoyer une liste des clefs composites à tester
            


            await Context.SaveChangesAsync();
            return updatedEntry.Entity;
        }

        /// <summary>
        /// Doit fournir la logique de sélection des états des tables d'associations
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entries"></param>
        private void test(Guid id, IEnumerable<EntityEntry> entries)
        {
            if (entries.Any())
            {
                // Récupération du nom des clefs de la tables d'association
                //var keyNames = Context.Model.FindEntityType(typeof(UserBeer)).FindPrimaryKey()
                // .Properties.Select(x => x.Name).ToList();
                //var keyNames = Context.Model.FindEntityType(entries.GetType().GetGenericArguments().First())
                //    .FindPrimaryKey().Properties.Select(x => x.Name).ToList();
                //var associationTableRows = Context.Set<UserBeer>().Where(at => at.)                

                foreach (var entry in entries)
                {

                }
            }
        }

        private void CustomTracking(T entity)
        {
            Context.ChangeTracker.TrackGraph(entity, node =>
             {
                 node.Entry.State = EntityState.Unchanged;

             if (node.Entry.Entity as Dictionary<string, object> != null)
             {
                 if (node.Entry.IsKeySet)
                     {
                         node.Entry.State = EntityState.Modified;
                     }
                     else
                     {
                         node.Entry.State = EntityState.Added;
                     }
                 }
             });
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

        private void SetEntriesState(IEnumerable<EntityEntry> entries, EntityState entityState)
        {
            if (entries.Any())
            {
                foreach (var entry in entries)
                {
                    entry.State = entityState;
                }
            }
        }

        //private void SetStateExceptSelfAndCollections(T entity, EntityState entityState)
        //{
        //    var entries = Context.ChangeTracker.Entries().Where(e => e.Entity != entity && e.Entity is not Dictionary<string, object>);

        //    if (entries.Any())
        //    {
        //        foreach (var entry in entries)
        //        {
        //            entry.State = entityState;
        //        }
        //    }
        //}

        /// <summary>
        /// TODO : à retravailler pour pouvoir être utilisable tt court (ne foncitonne pas avec les tables d'association
        /// custom)
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
