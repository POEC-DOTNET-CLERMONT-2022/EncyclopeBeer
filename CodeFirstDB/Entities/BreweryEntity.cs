using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Brewery")]
    public class BreweryEntity
    {
        [Key]
        [Column("BreweryId")] // Pas nécessaire si Id correctement nommée
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public CountryEntity Country { get; set; }

        public BreweryEntity()
        {
        }

        public BreweryEntity(string name, String description, CountryEntity country)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Country = country;
        }
    }
}
