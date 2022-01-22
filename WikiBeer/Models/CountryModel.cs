using System;

namespace Ipme.WikiBeer.Models
{
    public class CountryModel
    {
        public Guid Id { get; }

        public string Name { get; }

        public CountryModel(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}