namespace Ipme.WikiBeer.Entities
{
    public class BreweryEntity : IEntity
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public CountryEntity? Country { get; private set; }

        public IEnumerable<BeerEntity>? Beers { get; private set; }
        public BreweryEntity()
        {
        }

        public BreweryEntity(Guid id, string name, String description, 
            CountryEntity country, IEnumerable<BeerEntity>? beers)
        {
            Id = id;
            Name = name;
            Description = description;
            Country = country;
            Beers = beers;
        }
    }
}