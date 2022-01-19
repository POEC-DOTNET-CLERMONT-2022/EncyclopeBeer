using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class AdditiveEntity : IngredientEntity
    {
        public string Use { get; set; }

        public AdditiveEntity(string name, string description, string use) : base(name, description)
        {
            Use = use;
        }
    }
}
