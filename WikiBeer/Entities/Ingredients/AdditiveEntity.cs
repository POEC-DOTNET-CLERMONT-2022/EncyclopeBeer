using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Entities.Ingredients
{
    public class AdditiveEntity :IngredientEntity
    {
        public string Use { get; set; }

        private AdditiveEntity()
        {
        }

        public AdditiveEntity(Guid id, string name, string description, ICollection<BeerEntity> beers
            ,string use) 
            : base(id, name, description, beers)
        {
            Use = use;
        }
    }
}
