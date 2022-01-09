using AutoFixture;
using Ipme.WikiBeer.Model.Magic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ipme.WikiBeer.Model
{
    internal class BeerColor
    {
        public Guid Id { get; private set; }

        public string Name { get; internal set; }

        /// <summary>
        /// Peut être directement un Beer Manager au lieu d'une liste + méthodes CrUDe ???
        /// </summary>
        public List<Beer> CorrespondingBeers { get; internal set; }

        // TODO : Ajout du champ Country (doit être une classe)

        private readonly Fixture _fixture = new Fixture();

        public BeerColor(string name)
        {
            // Définitifs
            Id = Guid.NewGuid();
            Name = name;
            CorrespondingBeers = new List<Beer>();

            //Fixture
            CorrespondingBeers.AddRange(_fixture.CreateMany<Beer>(FixtureDefaultMagic.DEFAULT_BEERCOLORS_NUMBER));
        }
    }
}
