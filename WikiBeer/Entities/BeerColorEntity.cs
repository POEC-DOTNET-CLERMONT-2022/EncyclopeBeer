namespace Ipme.WikiBeer.Entities
{
    public class BeerColorEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        private BeerColorEntity()
        {
        }

        public BeerColorEntity(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}