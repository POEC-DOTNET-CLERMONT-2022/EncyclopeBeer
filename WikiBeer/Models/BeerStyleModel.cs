using AutoFixture;
using Ipme.WikiBeer.Models.Magic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ipme.WikiBeer.Models
{
    public class BeerStyleModel
    {
        public Guid Id { get; }
        public string Name { get; internal set; }
        public string Description { get; internal set; }

        public BeerStyleModel(string name, string description)
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
