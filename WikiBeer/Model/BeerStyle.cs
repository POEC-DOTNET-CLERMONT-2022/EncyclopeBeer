using AutoFixture;
using Ipme.WikiBeer.Model.Magic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ipme.WikiBeer.Model
{
    public class BeerStyle
    {
        public Guid Id { get; }
        public string Name { get; internal set; }
        public string Description { get; internal set; }

        private readonly Fixture _fixture = new Fixture();

        public BeerStyle(string name, string description = Rules.DEFAULT_BEERSTYLE_DESCRIPTION)
        {
            // Définitifs
            Id = Guid.NewGuid();
            Name = name;
            Description = description;

            //Fixture
            //CorrespondingBeers.AddRange(_fixture.CreateMany<Beer>(FixtureDefaultMagic.DEFAULT_BEERSTYLE_NUMBER));
        }
    }
}
