using Ipme.WikiBeer.Entities.Ingredients;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Ipme.WikiBeer.Persistance")]
[assembly: InternalsVisibleTo("Ipme.WikiBeer.API")]
namespace Ipme.WikiBeer.Entities
{
    public class BeerEntity : IEntity
    {
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float? Ibu { get; set; }
        public float Degree { get; set; }
        public BreweryEntity Brewery { get; set; }
        public BeerStyleEntity Style { get; set; }
        public BeerColorEntity Color { get; set; }
        /// <summary>
        /// ICollection car on aura peut être envie de faire des add ou delete dessus (par rapport à IEnumerable
        /// ou l'on ne peut que itérer)
        /// </summary>
        public IEnumerable<IngredientEntity> Ingredients { get; set; } 

        /// <summary>
        /// Constructeur par défaut nécessaire pour utilisation du new dans le 
        /// GenericBddRepository (d'ou le public -> que l'on devrait passer en internal)
        /// et pour utilisation de EFCore (pas certain de la validité de ce truc)
        /// </summary>            
        public BeerEntity()
        {
        }

        internal BeerEntity(Guid id, string name, float? ibu, float degree, BeerStyleEntity style,
            BeerColorEntity color, BreweryEntity brewery, IEnumerable<IngredientEntity> ingredients)
        {
            Id = id;
            Name = name;
            Ibu = ibu;
            Degree = degree;
            Style = style;
            Color = color;
            Brewery = brewery;
            Ingredients = ingredients;
        }       
    }
}