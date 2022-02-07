using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Entities.Ingredients
{
    public class HopEntity : IngredientEntity
    {
        public float AlphaAcid { get; private set; }

        private HopEntity(Guid id, string name, string description, float alphaAcid)
            : base(id, name, description)
        {
            AlphaAcid = alphaAcid;
        }

        public HopEntity(Guid id, string name, string description, float alphaAcid, IEnumerable<BeerEntity>? beers)
            : this(id, name, description, alphaAcid)
        {            
            Beers = beers;
        }
    }
}
