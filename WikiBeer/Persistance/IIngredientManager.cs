using Ipme.WikiBeer.Model.Ingredients;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ipme.WikiBeer.Persistance
{
    public interface IIngredientManager
    {
        IEnumerable<Ingredient> GetAllIngredient();
    }
}
