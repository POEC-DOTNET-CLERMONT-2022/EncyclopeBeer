namespace Ipme.WikiBeer.Entities
{
    public class BeerStyleEntity : IEntity
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string? Description { get; private set; }

        public IEnumerable<BeerEntity>? Beers { get; private set; } 

        private BeerStyleEntity(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public BeerStyleEntity(Guid id, string name, string description, IEnumerable<BeerEntity>? beers)
            : this(id, name, description)
        {                
            Beers = beers;
        }
    }
}