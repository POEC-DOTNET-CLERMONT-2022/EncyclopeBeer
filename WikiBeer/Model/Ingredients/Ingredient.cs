using Ipme.WikiBeer.Model.Magic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ipme.WikiBeer.Model.Ingredients
{
    public abstract class Ingredient
    {
        public Guid Id { get; }
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public string TypeOfIngredient { get; private set; }

        public Ingredient(string name, string desription = Rules.DEFAULT_INGREDIENT_DESCRIPTION)
        {
            // Définitif
            Id = Guid.NewGuid();
            Name = name;            
            Description = desription;

            // Pour test
            TypeOfIngredient = this.ToString(); 
        }
    }
}
