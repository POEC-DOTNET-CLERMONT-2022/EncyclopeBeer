using System;

namespace Ipme.WikiBeer.Models
{
    public class CountryModel
    {
        public Guid Id { get;}

        public string Name { get; set; }

        public CountryModel(string name): this(Guid.Empty, name)
        {
        }

        public CountryModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}