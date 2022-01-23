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
        {
            // Définitifs
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Country = country;
        }

    }
}
