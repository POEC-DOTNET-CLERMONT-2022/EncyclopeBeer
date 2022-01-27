using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Models.Ingredients
{
    public class AdditiveModel : IngredientModel
    {
        public string Use { get; internal set; }

        public AdditiveModel(string name, string description, string use)
            : this(Guid.Empty, name, description, use)
        {
        }

        public AdditiveModel(Guid id, string name, string description, string use) : base(name, description)
        {
            Use = use;
        }
    }
}
