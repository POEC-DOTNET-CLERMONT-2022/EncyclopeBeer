using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Entities.Ingredients
{
    public abstract class IngredientEntity : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<BeerEntity> Beers { get; set; } // nécessaire pour la table d'association

        protected IngredientEntity()
        {
        }

        protected IngredientEntity(Guid id, string name, string description, ICollection<BeerEntity> beers)
        {
            Id = id;
            Name = name;
            Description = description;
            Beers = beers;
        }
    }
}
