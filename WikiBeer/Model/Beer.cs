using AutoFixture;
using Ipme.WikiBeer.Model.Ingredients;
using Ipme.WikiBeer.Model.Magic;
using System;
using System.Collections.Generic;

namespace Ipme.WikiBeer.Model
{
    public class Beer
    {
        public Guid Id { get; }
        public string Name { get; internal set; }
        public float Ibu { get; internal set; }
        public float Degree { get; internal set; }
        public BeerStyle Style { get; internal set; }
        public BeerColor Color { get; internal set; }
        public Brewery Brewery { get; internal set; }
        public Image Image { get; internal set; }

        private readonly Fixture _fixture = new Fixture();
 
        public List<Ingredient> Ingredients { get; internal set; }

        public Beer(string name, float ibu, float degree, BeerStyle style, BeerColor color, Brewery brewery)
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
            Ingredients = new List<Ingredient>(); // marche même si Ingredient est abstract
            Ingredients.AddRange(_fixture.CreateMany<Hops>(FixtureDefaultMagic.DEFAULT_HOP_NUMBER));
            Ingredients.AddRange(_fixture.CreateMany<Additive>(FixtureDefaultMagic.DEFAULT_ADDITIVE_NUMBER));
            Ingredients.AddRange(_fixture.CreateMany<Cereal>(FixtureDefaultMagic.DEFAULT_CEREAL_NUMBER));
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