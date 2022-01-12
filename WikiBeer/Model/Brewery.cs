using AutoFixture;
using Ipme.WikiBeer.Model.Magic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ipme.WikiBeer.Model
{
    public class Brewery
    {
        public Guid Id { get;}        
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public Country Country { get; internal set; }

        private readonly Fixture _fixture = new Fixture();

        public Brewery(string name, string description = Rules.DEFAULT_BREWERY_DESCRIPTION)
        {
            // Définitifs
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            
            //Fixture
        }

    }
}
