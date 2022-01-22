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

        private BeerStyleEntity()
        {
        }
        public BeerStyleEntity(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

    }
}
