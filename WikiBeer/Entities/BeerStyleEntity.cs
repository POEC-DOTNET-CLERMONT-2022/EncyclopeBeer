namespace Ipme.WikiBeer.Entities
{
    public class BeerStyleEntity : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public BeerStyleEntity()
        {
        }

        public BeerStyleEntity(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}