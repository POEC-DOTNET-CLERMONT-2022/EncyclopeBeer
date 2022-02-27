using Ipme.WikiBeer.Entities;
using Ipme.WikiBeer.Entities.AssociationTables;
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
/// /// Sur comment récupérer une clef composite
/// https://stackoverflow.com/questions/30688909/how-to-get-primary-key-value-with-entity-framework-core
/// Sur l'eager loading (un peu plus)
/// https://stackoverflow.com/questions/49593482/entity-framework-core-2-0-1-eager-loading-on-all-nested-related-entities/49597502#49597502
/// Note pas la peine de retrhow explicitement les possible exception tant qu'on a pas de logger ici.
/// </summary>
namespace Ipme.WikiBeer.Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        protected DbContext Context { get; }
        protected readonly string _errInfo;

        public GenericRepository(DbContext context)
        {
            Context = context;
            _errInfo = $"From {this.GetType().Name}";
        }

        public virtual async Task<T> CreateAsync(T entityToCreate)
        {
            if (entityToCreate.Id != Guid.Empty)
                throw new UnauthorizedDbOperationException($"{_errInfo} : CreateAsync. Entity {typeof(T).Name}, " +
                    $"Id of enity canoot benull during");
            var newEntry = Context.Attach(entityToCreate);
            newEntry.State = EntityState.Added; // pour assurer l'ajout avec des Id non générés par la base
            await SaveChangesAsync(entityToCreate.Id);
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

        /// <summary>
        /// Récupère une entité.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="EntryNotFoundException"></exception>
        public virtual async Task<T> GetByIdAsync(Guid id)
        {     
            return await Context.Set<T>().FindAsync(id) 
                ?? throw new EntryNotFoundException($"{_errInfo} : GetByIdAsync. Entity {typeof(T).Name} Id : {id} not found in base.");       
        }

        public virtual async Task<T> GetByIdNoIncludeAsync(Guid id)
        {
            return await Context.Set<T>().IgnoreAutoIncludes().FirstOrDefaultAsync(obj => obj.Id == id)
                ?? throw new EntryNotFoundException($"{_errInfo} : GetByIdNoIncludeAsync. Entity {typeof(T).Name} Id : {id} not found in base.");
        }
      
        public virtual async Task<T> UpdateAsync(T entityToUpdate)
        {
            //var entryToUpdate = Context.Attach(entityToUpdate);
            var entryToUpdate = Context.Update(entityToUpdate);

            if (!Context.Set<T>().Any(e => e.Id == entityToUpdate.Id))
            {
                throw new EntryNotFoundException($"{_errInfo} : UpdateAsync. Entity {typeof(T).Name}, Id : {entityToUpdate.Id} not found in base.");
            }

            entryToUpdate.State = EntityState.Modified;           

            TryUpdateAssociationTables(entryToUpdate, entityToUpdate.Id);

            await SaveChangesAsync(entityToUpdate.Id);

            return entryToUpdate.Entity;
        }

        public virtual async Task DeleteByIdAsync(Guid id)
        {

            T entity = await GetByIdAsync(id);

            Context.Remove(entity);

            await SaveChangesAsync(id);
        }

        /// <summary>
        /// Sauvegarde les changements dans le context.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="callerMethodName"></param>
        /// <returns></returns>
        /// <exception cref="EntityRepositoryException"></exception>
        private async Task SaveChangesAsync(Guid id, [CallerMemberName] string callerMethodName = "")
        {
            var saveResponse = await Context.SaveChangesAsync();
            if (saveResponse == 0)
            {
                throw new EntityRepositoryException($"{_errInfo} : {callerMethodName}. Entity {typeof(T).Name}, " +
                    $"Id : {id}. No lines has changed in Database after calling DbContext.SaveChangeAsync().");
            }
        }

        /// <summary>
        /// Met à jour les tables d'associatiopns si elles existent.
        /// </summary>
        /// <param name="entryToUpdate"></param>
        /// <param name="associatedId"></param>
        private void TryUpdateAssociationTables(EntityEntry entryToUpdate, Guid associatedId)
        {
            // recherche des tables d'association dans l'entité
            var types = GetAssociationTableTypes(entryToUpdate);
            if (types.Any())
            {
                // Update si des tables ont été trouvées
                UpdateAssociationTables(types, associatedId);
            }
        }

        /// <summary>
        /// Retourne la liste des types IAssociationTable contenus dans l'entry donnée.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        private IEnumerable<Type> GetAssociationTableTypes(EntityEntry entry)
        {
            var propInfo = entry.Entity.GetType().GetProperties();
            var asTypes = new List<Type>();
            foreach (var prop in propInfo)
            {
                if (typeof(IEnumerable<IAssociationTable>).IsAssignableFrom(prop.PropertyType))
                    asTypes.Add(prop.PropertyType.GetGenericArguments().FirstOrDefault());
            }
            return asTypes;
        }

        /// <summary>
        /// Update les tables d'associations contenue dans types à partir de l'id de l'entité à update
        /// </summary>
        /// <param name="types"></param>
        /// <param name="id"></param>
        private void UpdateAssociationTables(IEnumerable<Type> types, Guid id)
        {
            var cpropInfo = Context.GetType().GetProperties();
            foreach (var type in types)
            {
                // entités correpondant au type de table
                var entries = Context.ChangeTracker.Entries().Where(e => e.Entity.GetType() == type);
                var inContextEntities = entries.Select(e => e.Entity);
                // récupération du bon DbSet en fonction du type de table
                var setInfo = cpropInfo.Where(pi => pi.PropertyType.GetGenericArguments().FirstOrDefault() == type).FirstOrDefault();
                var set = (IQueryable<IAssociationTable>?)setInfo?.GetValue(Context) 
                    ?? throw new EntityRepositoryException($"{_errInfo} : UpdateAssociationTable. DbSet is null");
                // entités en base 
                var inBaseEntities = set.AsNoTracking().AsEnumerable().Where(atr => atr.IsInCompositeKey(id));
                // Entries seulement en base passées en Deleted
                SetStateOnExcept(inBaseEntities, inContextEntities, EntityState.Deleted);
                // Entries seulement dans le context passées en Added
                SetStateOnExcept(inContextEntities, inBaseEntities, EntityState.Added);
            }
        }

        /// <summary>
        /// Affecte l'Entity state donné aux entries des entités de first non contenue dans second 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="state"></param>
        private void SetStateOnExcept(IEnumerable<object> first, IEnumerable<object> second, EntityState state)
        {
            var differenceSet = first.Except(second);
            foreach (var elem in differenceSet)
            {
                var entry = Context.Entry(elem);
                entry.State = state;
            }
        }

    }
}
