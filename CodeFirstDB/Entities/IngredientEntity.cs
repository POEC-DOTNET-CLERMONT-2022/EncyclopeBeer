using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Ingredient")]
    public abstract class IngredientEntity
    {
        [Key]
        [Column("IngredientId")]
        public Guid Id { get; set; }

        [MaxLength(50)] // Ou StringLength(x), voir comment du dessous pour la différence
        //[StringLength(50)] // minimum ET maximum
        public string Name { get; set; }

        /// <summary>
        /// TODO : Sortir les Magics comme ceux la dans une classe static Rules. (avec des const)
        /// </summary>
        [MaxLength(400)]
        public string Description { get; set; }

        // A commenter pour les tests fixtures
        //[ForeignKey("IngredientId")]
        //public ICollection<BeerEntity> Beers { get; set; } // nécessaire pour la table d'association

        protected IngredientEntity(string name, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }

    }
}
