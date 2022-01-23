using AutoFixture;
using AutoFixture.Kernel;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Pour test connection à l'API (la BDD est vide pour l'instant)
/// </summary>
namespace Perstistance
{
    public class FakeBeerRepository : IFakeBeerRepository
    {
        private List<BeerEntity> Beers { get; } = new List<BeerEntity>();
        private readonly Fixture _fixture = new Fixture();

        public FakeBeerRepository()
        {
            // Transforme tt les ingrédients en Hops
            _fixture.Customizations.Add
                (
                new TypeRelay(
                    typeof(IngredientEntity),
                    typeof(HopEntity)
                    )
                );

            // Frocer autofixture a passer dans le constructeur le plus élaboré
            //_fixture.Customize<BeerEntity>(c => c.FromFactory(
            //                new MethodInvoker(
            //                new GreedyConstructorQuery())));

            Beers.AddRange(_fixture.CreateMany<BeerEntity>(FixtureDefaultMagic.DEFAULT_BEER_NUMBER));
        }

        #region CRUD

        public BeerEntity Create(BeerEntity beerEntityToCreate)
        {
            Beers.Add(beerEntityToCreate);
            return beerEntityToCreate;
        }

        public IEnumerable<BeerEntity> GetAll()
        {
            return Beers;
        }

        /// <summary>
        /// Peut retourner un null si pas trouvé. TODO : implémenter un test sur ArgumentNUll Ou InvalidOperation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BeerEntity? GetById(Guid id)
        {
            return Beers.SingleOrDefault(beer => beer.Id == id);
        }
        public BeerEntity? GetByName(string name)
        {
            return Beers.SingleOrDefault(beer => beer.Name == name);
        }

        public bool DeleteById(Guid id)
        {
            BeerEntity? beerEntity = GetById(id);
            if (beerEntity == null)
                return false;

            Beers.Remove(beerEntity);
            return true;
        }
        #endregion
    }
}
