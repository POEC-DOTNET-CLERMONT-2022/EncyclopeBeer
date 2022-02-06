using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Entities.Ingredients
{
    public class CerealEntity : IngredientEntity
    {
        public float Ebc { get; private set; }

        private CerealEntity()
        {
        }

        public CerealEntity(Guid id, string name, string description, IEnumerable<BeerEntity>? beers
            , float ebc)
            : base(id, name, description, beers)
        {
            Ebc = ebc;
        }
    }
}
