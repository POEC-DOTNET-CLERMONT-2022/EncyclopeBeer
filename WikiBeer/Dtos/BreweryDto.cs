namespace Ipme.WikiBeer.Dtos
{
    public class BreweryDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CountryDto? Country { get; set; }
    }
}