namespace Ipme.WikiBeer.Dtos
{
    public class BeerStyleDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

    }
}