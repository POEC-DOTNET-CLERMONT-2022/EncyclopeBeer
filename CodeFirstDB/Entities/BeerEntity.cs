using AutoFixture;
using AutoFixture.Kernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Beer")]
    public class BeerEntity : IEntity
    { 
        [Key]
        [Column("BeerId")]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public float Ibu { get; set; }

        public float Degree { get; set; }

        public BreweryEntity Brewery { get; set; }

        public BeerStyleEntity Style { get; set; }

        public BeerColorEntity Color { get; set; }

        [ForeignKey("BeerId")] // pour nommage correct de la table d'association
        //public  ICollection<IngredientEntity> Ingredients { get; set; }
        public List<IngredientEntity> Ingredients { get; set; } // List juste pour test Fixture


        [NotMapped] // pour ne pas apparaitre en bdd
        private readonly Fixture _fixture = new Fixture();

        // Constructeur par défaut nécessaire pour héritage du BeerRepository à partir du GenericBddRepository
        public BeerEntity()
        {
            Id = Guid.NewGuid();
            // Fixture
            


            //Ingredients = new List<IngredientEntity>();
            //Ingredients.Add(_fixture.Create<HopEntity>());
            //Ingredients.Add(_fixture.Create<AdditiveEntity>());
            //Ingredients.Add(_fixture.Create<CerealEntity>());            //_fixture.Customizations.Add

            //Ingredients = new List<IngredientEntity>();
            //Ingredients.AddRange(_fixture.CreateMany<HopEntity>(1));
            //Ingredients.AddRange(_fixture.CreateMany<AdditiveEntity>(1));
            //Ingredients.AddRange(_fixture.CreateMany<CerealEntity>(1));            //_fixture.Customizations.Add
            //(
            //new TypeRelay(
            //    typeof(IngredientEntity),
            //    typeof(HopEntity)
            //    )
            //);
        }

        public BeerEntity(string name, float ibu, float degree, BeerStyleEntity style, BeerColorEntity color, BreweryEntity brewery)
        {
            // Définitif
            Id = Guid.NewGuid();
            Name = name;
            Ibu = ibu;
            Degree = degree;
            Style = style;
            Color = color;
            Brewery = brewery;

            // Fixture            
            Ingredients = new List<IngredientEntity>(); // marche même si Ingredient est abstract
            Ingredients.AddRange(_fixture.CreateMany<HopEntity>(1).ToList());
            Ingredients.AddRange(_fixture.CreateMany<AdditiveEntity>(1).ToList());
            Ingredients.AddRange(_fixture.CreateMany<CerealEntity>(1).ToList());
        }


    }
}
