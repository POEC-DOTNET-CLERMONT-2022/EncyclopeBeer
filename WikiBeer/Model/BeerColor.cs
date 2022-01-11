using AutoFixture;
using Ipme.WikiBeer.Model.Magic;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Gros Doute sur l'interet de cette classe : je remplacerai bien par une énum simple
/// </summary>
namespace Ipme.WikiBeer.Model
{
    internal class BeerColor
    {
        public Guid Id { get; }
        public string Name { get; internal set; }
        /// <summary>
        /// Peut être directement un Beer Manager au lieu d'une liste + méthodes CrUDe ???
        /// </summary>
        // public List<Beer> CorrespondingBeers { get; internal set; }

        private readonly Fixture _fixture = new Fixture();

        public BeerColor(string name)
        {
            // Définitifs
            Id = Guid.NewGuid();
            Name = name;
            //CorrespondingBeers = new List<Beer>();

            ////Fixture
            //CorrespondingBeers.AddRange(_fixture.CreateMany<Beer>(FixtureDefaultMagic.DEFAULT_BEERCOLOR_NUMBER));
        }
    }
}
