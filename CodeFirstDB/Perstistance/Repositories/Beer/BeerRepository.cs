using Entities;
using Microsoft.EntityFrameworkCore;
using Perstistance.Repositories;

/// <summary>
/// Repository pour CRUD sur bdd : à besoin d'une instance de DBcontext.
/// </summary>
namespace Perstistance
{
    public class BeerRepository : GenericDbRepository<BeerEntity>
    {
        private DbContext BeerContext { get; }

        //public BeerRepository()
        //{
        //}

        public BeerRepository(DbContext beerContext): base(beerContext)
        {
        }

        #region CRUD Spécifique aux BeerEntity
        //public IEnumerable<IngredientEntity> GetIngredients()
        //{

        //}
        #endregion
    }
}