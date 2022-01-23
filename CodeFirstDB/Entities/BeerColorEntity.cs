using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("BeerColor")]
    public class BeerColorEntity
    {
        [Key]
        [Column("ColorId")]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public BeerColorEntity(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
