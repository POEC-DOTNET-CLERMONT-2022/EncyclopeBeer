using AutoFixture;
using Ipme.WikiBeer.Models.Ingredients;
using System;
using System.Collections.Generic;

namespace Ipme.WikiBeer.Models
{
    public class BeerModel : ObservableObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Ibu { get; set; }
        public float Degree { get; set; }
        public BeerStyleModel Style { get; set; }
        public BeerColorModel Color { get; set; }
        public BreweryModel Brewery { get; set; }
        public IEnumerable<IngredientModel> Ingredients { get; set; }

        public BeerModel()
        {
        }

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

        // Copy contrsuctor
        public BeerModel(BeerModel beer)
        {
            Brewery = beer.Brewery;
            Style = beer.Style;
            Color = beer.Color;
            Degree = beer.Degree;
        }
    }
}