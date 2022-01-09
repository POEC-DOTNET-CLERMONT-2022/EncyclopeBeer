using AutoFixture;
using Ipme.WikiBeer.Model.Ingredients;
using Ipme.WikiBeer.Persistance.Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ipme.WikiBeer.Persistance
{
    public class IngredientManager : IIngredientManager
    {
        private readonly List<Ingredient> _ingredients;

        private readonly Fixture _fixture = new Fixture();

        public IngredientManager()
        {
            // Définitifs
            _ingredients = new List<Ingredient>();

            // Fixture
            _ingredients.AddRange(_fixture.CreateMany<Hops>(FixtureDefaultMagic.DEFAULT_HOPS_NUMBER));
            _ingredients.AddRange(_fixture.CreateMany<Additive>(FixtureDefaultMagic.DEFAULT_ADDITIVES_NUMBER));
            _ingredients.AddRange(_fixture.CreateMany<Cereal>(FixtureDefaultMagic.DEFAULT_CEREALS_NUMBER));
        }

        public void AddIngredient(Ingredient ingredient)
        {
            _ingredients.Add(ingredient);
        }

        public void DeleteIngredient(Ingredient ingredient)
        {
            if (!_ingredients.Any())
                _ingredients.Remove(ingredient);
        }

        public IEnumerable<Ingredient> GetAllIngredient()
        {
            return _ingredients;
        }

        /// <summary>
        /// A passer en générique pous plus de simplicité
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Hops> GetAllHop()
        {
            return _ingredients.OfType<Hops>();
        }

    }
}
