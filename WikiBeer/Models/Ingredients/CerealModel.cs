using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Models.Ingredients
{
    public class CerealModel : IngredientModel
    {
        public float Ebc { get; internal set; }

        public CerealModel(string name, string description, float ebc)
            :this(Guid.Empty, name, description, ebc)
        {
        }

        public CerealModel(Guid id, string name, string description, float ebc) : base(id, name, description)
        {
            Ebc = ebc;
        }
    }
}
