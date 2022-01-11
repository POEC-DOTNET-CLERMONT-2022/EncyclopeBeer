using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Dtos.Ingredients;
using Ipme.WikiBeer.Model.Ingredients;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ipme.WikiBeer.Extension.Factories
{
    public static class IngredientFactory
    {

        /// <summary>
        /// TODO : à refaire via LInQ
        /// </summary>
        /// <param name="ingredients"></param>
        /// <returns></returns>
        public static IEnumerable<IngredientDto> ToDto(this IEnumerable<Ingredient> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                yield return ingredient.ToDto();
            }
        }

        public static IngredientDto ToDto(this Ingredient ingredient)
        {   // TypeOfIngredient ici pour test
            return new IngredientDto { Id = ingredient.Id, Name = ingredient.Name, Description = ingredient.Description, TypeOfIngredient = ingredient.TypeOfIngredient }; 
        }
    }
}
