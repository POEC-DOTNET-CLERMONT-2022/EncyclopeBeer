namespace Ipme.WikiBeer.Entities
{
    public class BeerStyleEntity : IEntity
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string? Description { get; private set; }

        public IEnumerable<BeerEntity>? Beers { get; private set; } 

        public BeerStyleEntity()
        {
        }

        public BeerStyleEntity(Guid id, string name, string description, IEnumerable<BeerEntity>? beers)
        {
            Id = id;
            Name = name;
            Description = description;            
            Beers = beers;
        }
    }
}