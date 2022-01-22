namespace Ipme.WikiBeer.Entities
{
    public class BreweryEntity : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public CountryEntity Country { get; set; }

        private BreweryEntity()
        {
        }

        public BreweryEntity(Guid id, string name, String description, CountryEntity country)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Country = country;
        }
    }
}