using Ipme.WikiBeer.Dtos.Ingredients;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ipme.WikiBeer.Dtos 
{
    public class BeerDto : IDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public float? Ibu { get; set; }
        public float Degree { get; set; }
        public BreweryDto Brewery { get; set; }
        public BeerStyleDto Style { get; set; }
        public BeerColorDto Color { get; set; }
        public IEnumerable<IngredientDto> Ingredients { get; set; }
    }
}
