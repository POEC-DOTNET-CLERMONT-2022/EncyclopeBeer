using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Entities.Ingredients
{
    public class AdditiveEntity :IngredientEntity
    {
        public string Use { get; private set; }

        private AdditiveEntity(Guid id, string name, string description, string use)
            : base(id, name, description)
        {
            Use = use;
        }

        public AdditiveEntity(Guid id, string name, string description, string use, IEnumerable<BeerEntity>? beers) 
            : this(id, name, description, use)
        {
            Beers = beers;
        }
    }
}
