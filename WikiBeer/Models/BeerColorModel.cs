using AutoFixture;
using Ipme.WikiBeer.Models.Magic;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Gros Doute sur l'interet de cette classe : je remplacerai bien par une énum simple
/// </summary>
namespace Ipme.WikiBeer.Models
{
    public class BeerColorModel
    {
        public Guid Id { get; }
        public string Name { get; internal set; }

        public BeerColorModel(string name) : this(Guid.Empty, name)
        {
        }

        public BeerColorModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
