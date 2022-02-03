using Ipme.WikiBeer.Entities.Ingredients;
using System.Runtime.CompilerServices;

//[assembly: InternalsVisibleTo("Ipme.WikiBeer.Persistance")]
//[assembly: InternalsVisibleTo("Ipme.WikiBeer.API")]
namespace Ipme.WikiBeer.Entities
{
    public class BeerEntity : IEntity
    {
        
        public Guid Id { get; set; }
        public string Name { get; private set; }
        //public string? ALACON { get; private set; }
        public string? Description { get; private set; }
        public float? Ibu { get; private set; }
        public float Degree { get; private set; }        
        public BreweryEntity? Brewery { get; private set; }

        public Guid? StyleId { get; private set; }
        public BeerStyleEntity? Style { get; private set; }
        public BeerColorEntity? Color { get; private set; }
        public IEnumerable<IngredientEntity>? Ingredients { get; private set; } 

        /// <summary>
        /// Constructeur par défaut nécessaire pour utilisation du new dans le 
        /// GenericBddRepository (d'ou le public -> que l'on devrait passer en internal)
        /// et pour utilisation de EFCore (pas certain de la validité de ce truc)
        /// </summary>            
        internal BeerEntity(Guid id)
        {
            Id = id;
        }

        public BeerEntity(Guid id, string name, string? description, float? ibu, float degree, BeerStyleEntity? style,
            BeerColorEntity? color, BreweryEntity? brewery, IEnumerable<IngredientEntity>? ingredients)
        {
            Id = id;
            Name = name;
            Description = description;
            Ibu = ibu;
            Degree = degree;
            Style = style;
            Color = color;
            Brewery = brewery;
            Ingredients = ingredients;
        }       
    }
}