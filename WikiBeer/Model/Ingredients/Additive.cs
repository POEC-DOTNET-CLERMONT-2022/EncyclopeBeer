using System;
using System.Collections.Generic;
using System.Text;

namespace Ipme.WikiBeer.Model.Ingredients
{
    internal class Additive : Ingredient
    {
        public string Use { get; internal set; }

        public Additive(string name, string use) : base(name)
        {
            Use = use;
        }
    }
}
