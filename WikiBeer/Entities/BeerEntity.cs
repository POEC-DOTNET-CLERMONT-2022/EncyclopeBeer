using Ipme.WikiBeer.Entities.Ingredients;
using System.Runtime.CompilerServices;

/// <summary>
/// Notes sur les propriétés de naviguations dans les property : 
/// https://docs.microsoft.com/en-us/ef/core/modeling/constructors
/// EFCore ne peut pas set les propréiétés de navigation dans un constructeur
/// (d'ou le constructeur par défaut).
/// </summary>
//[assembly: InternalsVisibleTo("Ipme.WikiBeer.Persistance")]
//[assembly: InternalsVisibleTo("Ipme.WikiBeer.API")]
namespace Ipme.WikiBeer.Entities
{
    public class BeerEntity : IEntity
    {       
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public float? Ibu { get; private set; }
        public float Degree { get; private set; }        
        public BreweryEntity? Brewery { get; private set; }
        public BeerStyleEntity? Style { get; private set; }
        public BeerColorEntity? Color { get; private set; }

        public IEnumerable<Guid> IngredientIds { get; init; }
        public IEnumerable<IngredientEntity>? Ingredients { get; private set; }
        public IEnumerable<UserBeer>? UserBeers { get; private set; }
 
        private BeerEntity(Guid id, string name, string? description, float? ibu, float degree)
        {
            Id = id;
            Name = name;
            Description = description;
            Ibu = ibu;
            Degree = degree;
        }

        public BeerEntity(Guid id, string name, string? description, float? ibu, float degree, BeerStyleEntity? style,
            BeerColorEntity? color, BreweryEntity? brewery,
            IEnumerable<IngredientEntity>? ingredients, IEnumerable<UserBeer>? userBeers)
            : this(id, name, description, ibu, degree)
        {
            Style = style;
            Color = color;
            Brewery = brewery;
            Ingredients = ingredients;
            UserBeers = userBeers;
        }
    }
}