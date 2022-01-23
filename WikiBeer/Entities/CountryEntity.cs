namespace Ipme.WikiBeer.Entities
{
    public class CountryEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //public IEnumerable<BreweryEntity> Breweries { get; set; }

        private CountryEntity()
        {
        }

        public CountryEntity(Guid id, string name)//, IEnumerable<BreweryEntity> breweries)
        {
            Id = id;
            Name = name;
            //Breweries = breweries;
        }        
    }
}