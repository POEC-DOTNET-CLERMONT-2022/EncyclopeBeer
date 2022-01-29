using AutoFixture;
using Ipme.WikiBeer.Models.Magic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ipme.WikiBeer.Models
{
    public class BreweryModel
    {
        public Guid Id { get;}        
        public string Name { get; internal set; }
        public string Description { get; internal set; }
        public CountryModel Country { get; internal set; }

        public BreweryModel(string name, string description, CountryModel country)
            : this(Guid.Empty, name, description, country)
        {
        }

        public BreweryModel(Guid id, string name, string description, CountryModel country)
        {
            Id = id;
            Name = name;
            Description = description;
            Country = country;
        }
    }
}
