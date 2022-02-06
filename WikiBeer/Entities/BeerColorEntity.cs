namespace Ipme.WikiBeer.Entities
{
    public class BeerColorEntity : IEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public IEnumerable<BeerEntity>? Beers { get; private set; }
        public BeerColorEntity()
        {
        }

        public BeerColorEntity(Guid id, string name, IEnumerable<BeerEntity> beers)
        {
            Id = id;
            Name = name;
            Beers = beers;
        }
    }
}