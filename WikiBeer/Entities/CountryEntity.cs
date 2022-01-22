namespace Ipme.WikiBeer.Entities
{
    public class CountryEntity : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        private CountryEntity()
        {
        }

        public CountryEntity(Guid id, string name)
        {
            Id = id;
            Name = name;
        }        
    }
}