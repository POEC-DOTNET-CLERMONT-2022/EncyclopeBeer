using Ipme.WikiBeer.Entities.Ingredients;

namespace Ipme.WikiBeer.Entities
{
    public class BeerEntity : IEntity
    {
        
        public Guid Id { get; set; }

        public string Name { get; set; }

        public float Ibu { get; set; }

        public float Degree { get; set; }

        public BreweryEntity Brewery { get; set; }

        public BeerStyleEntity Style { get; set; }

        public BeerColorEntity Color { get; set; }

        public ICollection<IngredientEntity> Ingredients { get; set; } // List juste pour test Fixture

        /// <summary>
        /// Constructeur par défaut nécessaire pour héritage du BeerRepository à partir 
        /// du GenericBddRepository (d'ou le public) et pour utilisation de EFCore
        /// </summary>            
        public BeerEntity()
        {
        }

        public BeerEntity(Guid id, string name, float ibu, float degree, BeerStyleEntity style, BeerColorEntity color, BreweryEntity brewery)
        {
            Id = id;
            Name = name;
            Ibu = ibu;
            Degree = degree;
            Style = style;
            Color = color;
            Brewery = brewery;
        }       
    }
}