using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("BeerStyle")]
    public class BeerStyleEntity
    {
        [Key]
        [Column("StyleId")]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public BeerStyleEntity(string name)
        {
            Id = Guid.NewGuid();
            Name = Name;
        }

    }
}
