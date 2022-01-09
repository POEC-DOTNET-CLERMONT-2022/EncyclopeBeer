using AutoFixture;
using Ipme.WikiBeer.Model.Ingredients;
using System;
using System.Collections.Generic;
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
            _ingredients.AddRange(_fixture.CreateMany<Hops>(FixtureDefaultMagic.DEFAULT_INGREDIENT_NUMBER));
        }

        public IEnumerable<Ingredient> GetAllIngredient()
        {
            return _ingredients;
        }
    }
}
