namespace Dtos
{
    public class BeerDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public float Ibu { get; set; }

        public float Degree { get; set; }

        public BreweryDto Brewery { get; set; }

        public BeerStyleDto Style { get; set; }

        public BeerColorDto Color { get; set; }

        public IEnumerable<IngredientDto> Ingredients { get; set; }

        public BeerDto(string name, float ibu, float degree, BreweryDto brewery, 
            BeerStyleDto style, BeerColorDto color, IEnumerable<IngredientDto> ingredients)
        {
            Name = name;
            Ibu = ibu;
            Degree = degree;
            Brewery = brewery;
            Style = style; 
            Color = color;
            Ingredients = ingredients;
        }
    }
}