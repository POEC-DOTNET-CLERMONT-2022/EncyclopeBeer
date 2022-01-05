using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiBeer.Models
{
    internal class Additive : Ingredient
    {
        public string Use { get; internal set; }

        public Additive(string name, string use): base(name)
        {
            Use = use;
        }
    }
}
