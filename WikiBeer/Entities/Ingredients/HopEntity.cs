using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Entities.Ingredients
{
    public class HopEntity : IngredientEntity
    {
        public float AlphaAcid { get; set; }

        private HopEntity()
        {
        }

        public HopEntity(Guid id, string name, string description, ICollection<BeerEntity> beers
            , float alphaAcid)
            : base(id, name, description, beers)
        {
            AlphaAcid = alphaAcid;
        }
    }
}
