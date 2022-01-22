using Ipme.WikiBeer.Model.Magic;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// TODO : 
/// </summary>
namespace Ipme.WikiBeer.Models.Ingredients
{
    public abstract class IngredientModel
    {
        public Guid Id { get; }
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public string TypeOfIngredient { get; private set; } // Pour test pour l'instant 

        public IngredientModel(string name, string desription = Rules.DEFAULT_INGREDIENT_DESCRIPTION)
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
