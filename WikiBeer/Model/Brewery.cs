using AutoFixture;
using Ipme.WikiBeer.Model.Magic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ipme.WikiBeer.Model
{
    internal class Brewery
    {
        public Guid Id { get; private set; }
        
        public string Name { get; internal set; }

        public string Description { get; internal set; }

        /// <summary>
        /// Peut être directement un Beer Manager au lieu d'une liste + méthodes CrUDe ???
        /// </summary>
        public List<Beer> ProductedBeers { get; internal set; }

        // TODO : Ajout du champ Country (doit être une classe)

        private readonly Fixture _fixture = new Fixture();

        public Brewery(string name, string description = Rules.DEFAULT_BREWERY_DESCRIPTION)
        {
            // Définitifs
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            ProductedBeers = new List<Beer>();
            
            //Fixture
            ProductedBeers.AddRange(_fixture.CreateMany<Beer>(FixtureDefaultMagic.DEFAULT_BEER_NUMBER_BY_BREWERY));
        }

    }
}
