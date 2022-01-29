using AutoFixture;
using Ipme.WikiBeer.Models.Ingredients;
using System;
using System.Collections.Generic;

/// <summary>
/// Pour de la deep copy 
/// voir : // voir : https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone?redirectedfrom=MSDN&view=net-6.0#System_Object_MemberwiseClone
/// </summary>
namespace Ipme.WikiBeer.Models
{
    public class BeerModel : ObservableObject
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public float Ibu { get; set; }
        public float Degree { get; set; }
        public BeerStyleModel Style { get; set; }
        public BeerColorModel Color { get; set; }
        public BreweryModel Brewery { get; set; }
        public IEnumerable<IngredientModel> Ingredients { get; set; }

        public BeerModel(string name, float ibu, float degree, BeerStyleModel style,
            BeerColorModel color, BreweryModel brewery, IEnumerable<IngredientModel> ingredients)
            : this(Guid.Empty, name, ibu, degree, style, color, brewery, ingredients)
        {
        }

        public BeerModel(Guid id, string name, float ibu, float degree, BeerStyleModel style, 
            BeerColorModel color, BreweryModel brewery, IEnumerable<IngredientModel> ingredients)
        {
            Id = id;
            Name = name;
            Ibu = ibu;
            Degree = degree;
            Style = style;
            Color = color;
            Brewery = brewery;
            Ingredients = ingredients;
        }

        /// <summary>
        /// copy constructor (pour BeerList du Wpf -> a revoir en profondeur ce truc...)
        /// </summary>
        /// <param name="beer"></param>
        public BeerModel(BeerModel beer)
        {
            Id = beer.Id;
            Name = beer.Name; // attention string est aussi un type référence... mais changer string
                              // revient à en créer une nouvelle donc peut etre traité ici comme un type value
            Ibu = beer.Ibu;
            Degree = beer.Degree;
            // Attention ce n'est pas de la deep copy, 
            // juste de la copy par référence.
            // On devrait passer par new et avoir des copy constructor
            // pour ces objet aussi
            
            Style = beer.Style;
            Color = beer.Color;
            Brewery = beer.Brewery;
            Ingredients = beer.Ingredients;
        }
    }
}