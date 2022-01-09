using AutoFixture;
using Ipme.WikiBeer.Model.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ipme.WikiBeer.Persistance
{
    public class IngredientManager : IIngredientManager
    {
        private List<Ingredient> _ingredients;

        private readonly Fixture _fixture = new Fixture();

        public IngredientManager()
        {
            _ingredients = new List<Ingredient>();
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
        /// A passé en générique pous plus de simplicité
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Hops> GetAllHops()
        {
            return _ingredients.OfType<Hops>();
        }

    }
}
