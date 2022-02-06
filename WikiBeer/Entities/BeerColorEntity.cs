namespace Ipme.WikiBeer.Entities
{
    public class BeerColorEntity : IEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<BeerEntity>? Beers { get; private set; }

        private BeerColorEntity(Guid id, string name) 
        {
            Id = id;
            Name = name;
        }
  
        public BeerColorEntity(Guid id, string name, IEnumerable<BeerEntity>? beers)
         : this(id, name)
        {            
            Beers = beers;
        }
    }
}