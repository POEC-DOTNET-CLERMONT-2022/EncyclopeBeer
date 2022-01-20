using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{ 
    public class BeerModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public float Ibu { get; set; }

        public float Degree { get; set; }

        public BreweryModel Brewery { get; set; }

        public BeerStyleModel Style { get; set; }

        public BeerColorModel Color { get; set; }

        public IEnumerable<IngredientModel> Ingredients { get; set; }

        public BeerModel()
        {
        }

        public BeerModel(string name, float ibu, float degree, BreweryModel brewery,
            BeerStyleModel style, BeerColorModel color, IEnumerable<IngredientModel> ingredients)
        {
            Name = name;
            Ibu = ibu;
            Degree = degree;
            Brewery = brewery;
            Style = style;
            Color = color;
            Ingredients = ingredients;
        }
    }
}
