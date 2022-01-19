using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Country")]
    public class CountryEntity
    {
        [Key]
        [Column("CountryId")]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public CountryEntity(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

    }
}
