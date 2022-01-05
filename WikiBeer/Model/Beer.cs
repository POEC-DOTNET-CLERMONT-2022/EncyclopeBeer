using AutoFixture;
using Ipme.WikiBeer.Model.Ingredients;
using System;
using System.Collections.Generic;

namespace Ipme.WikiBeer.Model
{
    public class Beer
    {
        public Guid Id { get; private set; }
        public string Name { get; internal set; }
        public float Ibu { get; internal set; }
        public float Degree { get; internal set; }

        private readonly Fixture _fixture = new Fixture();
        public List<Ingredient> _ingredients;

        public List<Ingredient> Ingredients
        {
            get { return _ingredients; }
            set { _ingredients = value; }
        }
        //public List<Ingredient> Ingredients { get; internal set; }

        /// <summary>
        /// TODO : Résoudre le bug Fixture (voir comment plus bas sur property Vs attribut)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ibu"></param>
        /// <param name="degree"></param>
        public Beer(string name, float ibu, float degree)
        {
            Id = Guid.NewGuid();
            Name = name;
            Ibu = ibu;
            Degree = degree;
            //Ingredients = ingredients;
            _ingredients = new List<Ingredient>(); // marche si Ingredient n'est pas abstract 
            _ingredients.AddRange(_fixture.CreateMany<Hops>(FixtureDefaultMagic.DEFAULT_HOPS_NUMBER));
            //Ingredients = new List<Ingredient>(); // marche même si Ingredient est abstract
            //Ingredients.AddRange(_fixture.CreateMany<Hops>(2));
        }

        public override string ToString()
        {
            return $" Name: {Name} - IBU: {Ibu} - Degree: {Degree}%";
        }

        public void TransferId(Beer new_beer)
        {
            if (new_beer == null)
            {
                return;
            }
            else
            {
                new_beer.Id = Id;
            }
        }

    }
}