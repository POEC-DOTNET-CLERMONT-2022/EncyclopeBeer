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

        public BeerStyleModel(string name, string description) : this(Guid.Empty, name, description)
        {
        }

        public BeerStyleModel(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
