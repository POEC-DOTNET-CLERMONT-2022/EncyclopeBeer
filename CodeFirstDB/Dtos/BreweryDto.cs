namespace Dtos
{
    public class BreweryDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public CountryDto Country { get; set; }
        
        /// TODO : Rajouter une ICollection de Beer (un brasseur doit savoir quelles bières il produit)

    }
}