using System;

namespace Ipme.WikiBeer.Model
{
    public class Country
    {
        public Guid Id { get; }

        public string Name { get; }

        public Country(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}