using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Entities.Ingredients
{
    public abstract class IngredientEntity : IEntity
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public IEnumerable<BeerEntity>? Beers { get; protected set; } // nécessaire pour la table d'association

        protected IngredientEntity(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public IngredientEntity(Guid id, string name, string description, IEnumerable<BeerEntity>? beers)
            : this(id, name, description)
        {            
            Beers = beers;
        }
    }
}
