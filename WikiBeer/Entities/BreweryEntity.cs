namespace Ipme.WikiBeer.Entities
{
    public class BreweryEntity : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public CountryEntity Country { get; set; }

        public ICollection<BeerEntity> Beers { get; set; }

        private BreweryEntity()
        {
        }

        public BreweryEntity(Guid id, string name, String description, 
            CountryEntity country, ICollection<BeerEntity> beers)
        {
            Id = id;
            Name = name;
            Description = description;
            Country = country;
            Beers = Beers;
        }
    }
}