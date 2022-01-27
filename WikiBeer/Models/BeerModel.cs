using AutoFixture;
using Ipme.WikiBeer.Models.Ingredients;
using System;
using System.Collections.Generic;

namespace Ipme.WikiBeer.Models
{
    public class BeerModel
    {
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

        public BeerModel(BeerModel beer)
        {
            Brewery = beer.Brewery;
            Style = beer.Style;
            Color = beer.Color;
            Degree = beer.Degree;
        }
    }
}