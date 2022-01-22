using AutoFixture;
using Ipme.WikiBeer.Models.Ingredients;
using System;
using System.Collections.Generic;

namespace Ipme.WikiBeer.Models
{
    public class BeerModel
    {
        public Guid Id { get; }
        public string Name { get; internal set; }
        public float Ibu { get; internal set; }
        public float Degree { get; internal set; }
        public BeerStyleModel Style { get; internal set; }
        public BeerColorModel Color { get; internal set; }
        public BreweryModel Brewery { get; internal set; }
        //public Image Image { get; internal set; }

        private readonly Fixture _fixture = new Fixture();
 
        public List<IngredientModel> Ingredients { get; internal set; }

        public BeerModel(string name, float ibu, float degree, BeerStyleModel style, BeerColorModel color, BreweryModel brewery)
        {
            // Définitif
            Id = Guid.NewGuid();
            Name = name;
            Ibu = ibu;
            Degree = degree;
            Style = style;
            Color = color;
            Brewery = brewery;

            // Fixture            
            //Ingredients = new List<Ingredient>(); // marche même si Ingredient est abstract
            //Ingredients.AddRange(_fixture.CreateMany<Hops>(FixtureDefaultMagic.DEFAULT_HOP_NUMBER));
            //Ingredients.AddRange(_fixture.CreateMany<Additive>(FixtureDefaultMagic.DEFAULT_ADDITIVE_NUMBER));
            //Ingredients.AddRange(_fixture.CreateMany<Cereal>(FixtureDefaultMagic.DEFAULT_CEREAL_NUMBER));
        }

        /// <summary>
        /// TODO : à refaire complètement
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $" Name: {Name} - IBU: {Ibu} - Degree: {Degree}%";
        }

    }
}