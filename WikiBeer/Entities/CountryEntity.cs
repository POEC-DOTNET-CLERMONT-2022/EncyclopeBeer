namespace Ipme.WikiBeer.Entities
{
    public class CountryEntity : IEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<BreweryEntity>? Breweries { get; set; }

        public CountryEntity()
        {
        }

        public CountryEntity(Guid id, string name, IEnumerable<BreweryEntity>? breweries)
        {
            Id = id;
            Name = name;
            Breweries = breweries;
        }        
    }
}