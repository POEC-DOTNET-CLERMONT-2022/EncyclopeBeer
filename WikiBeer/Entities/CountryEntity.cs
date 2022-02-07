namespace Ipme.WikiBeer.Entities
{
    public class CountryEntity : IEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<BreweryEntity>? Breweries { get; set; }

        public CountryEntity(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public CountryEntity(Guid id, string name, IEnumerable<BreweryEntity>? breweries)
            : this(id, name)
        {
            Breweries = breweries;
        }        
    }
}