using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Model.Ingredients;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ipme.WikiBeer.Extension.Factories
{
    public static class IngredientFactory
    {
        public static IEnumerable<IngredientDto> ToDto(this IEnumerable<Ingredient> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                yield return ingredient.ToDto();
            }
        }

        public static IngredientDto ToDto(this Ingredient ingredient)
        {
            //return new IngredientDto { Id = ingredient.Id, Name = ingredient.Name};
            return new IngredientDto { Id = ingredient.Id, Name = ingredient.Name, TypeOfIngredient = ingredient.TypeOfIngredient }; // Pour test classe asbtract
        }
    }
}
