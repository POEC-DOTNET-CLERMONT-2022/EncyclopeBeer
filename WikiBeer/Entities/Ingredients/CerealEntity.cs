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

        private CerealEntity(Guid id, string name, string description, float ebc)
           : base(id, name, description)
        {
            Ebc = ebc;
        }
        public CerealEntity(Guid id, string name, string description, float ebc, IEnumerable<BeerEntity>? beers)
            : this(id, name, description, ebc)
        {
            Beers = beers;
        }
    }
}
